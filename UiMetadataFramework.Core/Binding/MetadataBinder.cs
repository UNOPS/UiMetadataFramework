namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// This class holds mappings between server-side types and client-side UI controls. 
	/// It provides a number of APIs to simplify creation of metadata.
	/// </summary>
	public class MetadataBinder
	{
		/// <summary>
		/// <see cref="IServiceProvider"/> instance used when/if necessary.
		/// </summary>
		public readonly IServiceProvider Container;

		private readonly ConcurrentDictionary<Type, IEnumerable<InputFieldMetadata>> inputFieldMetadataCache = new();
		private readonly ConcurrentDictionary<Type, InputFieldBinding> inputFieldMetadataMap = new();
		private readonly object key = new();
		private readonly ConcurrentDictionary<Type, IEnumerable<OutputFieldMetadata>> outputFieldMetadataCache = new();
		private readonly ConcurrentDictionary<Type, OutputFieldBinding> outputFieldMetadataMap = new();
		private readonly List<string> registeredAssemblies = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures
		/// <see cref="DependencyInjectionContainer.Default"/> to be responsible for instantiating
		/// <see cref="InputFieldBinding"/> and <see cref="OutputFieldBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder()
			: this(DependencyInjectionContainer.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures the given
		/// <see cref="DependencyInjectionContainer"/> to be responsible for instantiating
		/// <see cref="InputFieldBinding"/> and <see cref="OutputFieldBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder(IServiceProvider container)
		{
			this.Container = container;
		}

		/// <summary>
		/// Gets list of all registered <see cref="InputFieldBinding"/>.
		/// </summary>
		public IReadOnlyDictionary<Type, InputFieldBinding> InputFieldBindings => this.inputFieldMetadataMap.AsReadOnlyDictionary();

		/// <summary>
		/// Gets list of all registered <see cref="OutputFieldBinding"/>.
		/// </summary>
		public IReadOnlyDictionary<Type, OutputFieldBinding> OutputFieldBindings => this.outputFieldMetadataMap.AsReadOnlyDictionary();

		/// <summary>
		/// Gets id of the form.
		/// </summary>
		/// <param name="formType">Type representing the form.</param>
		/// <returns>Id of the form.</returns>
		public static string GetFormId(Type formType)
		{
			var attribute = formType.GetTypeInfo().GetCustomAttributeSingleOrDefault<FormAttribute>();

			if (attribute == null)
			{
				throw new BindingException(
					$"Type '{formType.FullName}' does not have mandatory attribute '{typeof(FormAttribute).FullName}'.");
			}

			return GetFormId(formType, attribute);
		}

		/// <summary>
		/// Registers given <see cref="OutputFieldBinding"/> instance.
		/// </summary>
		/// <param name="binding"><see cref="OutputFieldBinding"/> instance.</param>
		public void AddBinding(OutputFieldBinding binding)
		{
			var existingBinding = this.outputFieldMetadataMap.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);

			if (existingBinding != null)
			{
				if (existingBinding.Equals(binding))
				{
					return;
				}

				throw new BindingException(
					$"Multiple output field bindings are trying to use client type '{binding.ClientType}'. " +
					"Each binding must have a unique client type.");
			}

			foreach (var serverType in binding.ServerTypes)
			{
				if (this.outputFieldMetadataMap.ContainsKey(serverType))
				{
					throw new InvalidOperationException(
						$"Type '{binding.ServerTypes}' is already bound to output field control '{binding.ClientType}'.");
				}

				if (serverType.GetTypeInfo().IsValueType)
				{
					// Bind nullable version of the value type.
					// For example when binding "int", also bind "int?".
					var nullable = typeof(Nullable<>).MakeGenericType(serverType);

					this.outputFieldMetadataMap.TryAdd(nullable, binding);
				}

				this.outputFieldMetadataMap.TryAdd(serverType, binding);
			}
		}

		/// <summary>
		/// Registers given <see cref="InputFieldBinding"/> instance.
		/// </summary>
		/// <param name="binding"><see cref="InputFieldBinding"/> instance.</param>
		public void AddBinding(InputFieldBinding binding)
		{
			var existingBinding = this.inputFieldMetadataMap.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);

			if (existingBinding != null)
			{
				if (existingBinding.Equals(binding))
				{
					return;
				}

				throw new BindingException(
					$"Bindings '{binding.GetType().FullName}' and '{existingBinding.GetType().FullName}' " +
					$"indicate same client type '{binding.ClientType}'. Each binding must have a unique client type.");
			}

			foreach (var serverType in binding.ServerTypes)
			{
				if (this.inputFieldMetadataMap.ContainsKey(serverType))
				{
					throw new InvalidOperationException(
						$"Type '{binding.ServerTypes}' is already bound to input field control '{binding.ClientType}'.");
				}

				if (serverType.GetTypeInfo().IsValueType)
				{
					// Bind nullable version of the value type.
					// For example when binding "int", also bind "int?".
					var nullable = typeof(Nullable<>).MakeGenericType(serverType);

					this.inputFieldMetadataMap.TryAdd(nullable, binding);
				}

				this.inputFieldMetadataMap.TryAdd(serverType, binding);
			}
		}

		/// <summary>
		/// Binds specified "server-side" type to the specified "client-side" input control type.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the output field.</param>
		/// <typeparam name="TServerType">Type to bind to a specific client control.</typeparam>
		public void AddInputFieldBinding<TServerType>(string clientType)
		{
			this.AddBinding(new InputFieldBinding(typeof(TServerType), clientType));
		}

		/// <summary>
		/// Binds specified "server-side" type to the specified "client-side" output control type.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the output field.</param>
		/// <param name="mandatoryCustomProperty">Mandatory custom property for the component.</param>
		/// <typeparam name="TServerType">Type to bind to a specific client control.</typeparam>
		public void AddOutputFieldBinding<TServerType>(string clientType, Type? mandatoryCustomProperty)
		{
			this.AddBinding(new OutputFieldBinding(typeof(TServerType), clientType, mandatoryCustomProperty));
		}

		/// <summary>
		/// Gets form metadata for the specified form.
		/// </summary>
		/// <typeparam name="TForm">Type representing the form.</typeparam>
		/// <typeparam name="TRequest">Type representing request for the form. 
		/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</typeparam>
		/// <typeparam name="TResponse">Type representing response of the form. 
		/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</typeparam>
		/// <returns><see cref="FormMetadata"/> instance.</returns>
		public FormMetadata BindForm<TForm, TRequest, TResponse>()
		{
			return this.BindForm(typeof(TForm), typeof(TRequest), typeof(TResponse));
		}

		/// <summary>
		/// Gets form metadata for the specified form.
		/// </summary>
		/// <param name="formType"> name="TForm">Type representing the form.</param>
		/// <param name="requestType">Type representing request for the form. 
		/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</param>
		/// <param name="responseType">Type representing response of the form. 
		/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</param>
		/// <returns><see cref="FormMetadata"/> instance.</returns>
		public FormMetadata BindForm(
			Type formType,
			Type requestType,
			Type responseType)
		{
			return new FormMetadata(this, formType, requestType, responseType);
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as input field(s).</typeparam>
		/// <param name="strict">If true, then only properties that have <see cref="InputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BindInputFields<T>(bool strict = false)
		{
			return this.BindInputFields(typeof(T), strict);
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as input field(s).</param>
		/// <param name="strict">If true, then only properties that have <see cref="InputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BindInputFields(Type type, bool strict = false)
		{
			return this.inputFieldMetadataCache.GetOrAdd(
				type,
				t => this.BindInputFieldsInternal(t, strict));
		}

		/// <summary>
		/// Builds metadata for the given output component with the provided list of custom properties. 
		/// </summary>
		/// <param name="type">Type of output component.</param>
		/// <param name="customProperties">List of custom properties to apply.</param>
		/// <exception cref="BindingException">Thrown if a mandatory custom property is missing.</exception>
		public OutputFieldMetadata BindOutputField(Type type, params ICustomPropertyAttribute[] customProperties)
		{
			var binding = this.GetOutputFieldBinding(type);

			if (binding.MandatoryCustomProperty != null)
			{
				var matchingCustomProperties = customProperties
					.Where(t => t.GetType().ImplementsClass(binding.MandatoryCustomProperty))
					.ToList();

				if (matchingCustomProperties.Count == 0)
				{
					throw new BindingException(
						$"Type '{type.FullName}' requires mandatory custom property " +
						$"of type '{binding.MandatoryCustomProperty.FullName}'.");
				}

				if (matchingCustomProperties.Count > 1)
				{
					throw new BindingException(
						$"Multiple custom properties of type '{binding.MandatoryCustomProperty.FullName}' provided. " +
						"Only one instance of the custom property is allowed.");
				}
			}

			return new OutputFieldMetadata(binding.ClientType)
			{
				Id = null,
				CustomProperties = customProperties.ToDictionary(t => t.Name, t => (object?)t.GetValue(type, this))
			};
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as output field(s).</param>
		/// <param name="strict">If true, then only properties that have <see cref="OutputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BindOutputFields(Type type, bool strict = false)
		{
			return this.outputFieldMetadataCache.GetOrAdd(
				type,
				t => this.BindOutputFieldsInternal(t, strict));
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as output field(s).</typeparam>
		/// <param name="strict">If true, then only properties that have <see cref="OutputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BindOutputFields<T>(bool strict = false)
		{
			return this.BindOutputFields(typeof(T), strict);
		}

		/// <summary>
		/// Scans assembly for implementations of <see cref="OutputFieldBinding"/>, <see cref="InputFieldBinding"/>
		/// and registers them in this instance of <see cref="MetadataBinder"/>.
		/// </summary>
		/// <param name="assembly">Assembly to scan.</param>
		public void RegisterAssembly(Assembly assembly)
		{
			// Avoid registering the same assembly twice.
			lock (this.key)
			{
				if (this.registeredAssemblies.Contains(assembly.FullName))
				{
					return;
				}

				this.registeredAssemblies.Add(assembly.FullName);
			}

			var outputFieldBindings = assembly.ExportedTypes
				.Where(
					t =>
					{
						var typeInfo = t.GetTypeInfo();
						return typeInfo.IsClass &&
							!typeInfo.IsAbstract &&
							typeInfo.IsSubclassOf(typeof(OutputFieldBinding));
					})
				.Select(t => this.Container.GetService(t))
				.Cast<OutputFieldBinding>();

			foreach (var binding in outputFieldBindings)
			{
				this.AddBinding(binding);
			}

			assembly.ExportedTypes.ForEach(
				t =>
				{
					var attribute = t.GetTypeInfo().GetCustomAttributeSingleOrDefault<OutputFieldTypeAttribute>();

					if (attribute != null)
					{
						this.AddBinding(new OutputFieldBinding(t, attribute));
					}
				});

			var inputFieldBindings = assembly.ExportedTypes
				.Where(
					t =>
					{
						var typeInfo = t.GetTypeInfo();
						return typeInfo.IsClass &&
							!typeInfo.IsAbstract &&
							typeInfo.IsSubclassOf(typeof(InputFieldBinding));
					})
				.Select(t => this.Container.GetService(t))
				.Cast<InputFieldBinding>();

			foreach (var binding in inputFieldBindings)
			{
				this.AddBinding(binding);
			}

			assembly.ExportedTypes.ForEach(
				t =>
				{
					var attribute = t.GetTypeInfo().GetCustomAttributeSingleOrDefault<InputFieldTypeAttribute>();

					if (attribute != null)
					{
						this.AddBinding(new InputFieldBinding(t, attribute));
					}
				});
		}

		internal static string GetFormId(Type formType, FormAttribute formAttribute)
		{
			return !string.IsNullOrWhiteSpace(formAttribute.Id)
				? formAttribute.Id!
				: formType.FullName ?? throw new BindingException($"Cannot form ID for type `{formType}`.");
		}

		private IEnumerable<InputFieldMetadata> BindInputFieldsInternal(Type type, bool strict = false)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttributeSingleOrDefault<InputFieldAttribute>();

				if (strict && attribute == null)
				{
					continue;
				}

				var binding = this.GetInputFieldBinding(
					property.PropertyType,
					$"{property.DeclaringType?.FullName}.{property.Name}");

				if (binding.MandatoryAttribute != null &&
					attribute?.GetType().ImplementsClass(binding.MandatoryAttribute) != true)
				{
					throw new BindingException(
						$"Property '{type.FullName}.{property.Name}' is missing a mandatory attribute " +
						$"of type '{binding.MandatoryAttribute.FullName}'.");
				}

				attribute ??= new InputFieldAttribute
				{
					Required = false,
					Hidden = false
				};

				var metadata = attribute.GetMetadata(property, binding, this);

				yield return metadata;
			}
		}

		private IEnumerable<OutputFieldMetadata> BindOutputFieldsInternal(Type type, bool strict = false)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttributeSingleOrDefault<OutputFieldAttribute>();

				if (strict && attribute == null)
				{
					continue;
				}

				var binding = this.GetOutputFieldBinding(
					property.PropertyType,
					$"{property.DeclaringType}.{property.Name}");

				if (binding.MandatoryCustomProperty != null)
				{
					var customProperty = property.GetCustomAttributes(binding.MandatoryCustomProperty).ToList();

					if (customProperty.Count == 0)
					{
						throw new BindingException(
							$"Property '{type.FullName}.{property.Name}' is missing a mandatory custom property " +
							$"of type '{binding.MandatoryCustomProperty.FullName}'.");
					}

					if (customProperty.Count > 1)
					{
						throw new BindingException(
							$"Property '{type.FullName}.{property.Name}' has multiple custom properties " +
							$"of type '{binding.MandatoryCustomProperty.FullName}'. Only one instance of the attribute is allowed.");
					}
				}

				attribute ??= new OutputFieldAttribute();

				yield return attribute.GetMetadata(property, binding, this);
			}
		}

		private InputFieldBinding GetInputFieldBinding(Type type, string? location = null)
		{
			var componentType = type.IsConstructedGenericType && !type.IsNullabble()
				? type.GetGenericTypeDefinition()
				: type;

			if (!this.inputFieldMetadataMap.TryGetValue(componentType, out InputFieldBinding binding))
			{
				var message = !string.IsNullOrWhiteSpace(location)
					? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any input field control."
					: $"Type '{type.FullName}' is not bound to any input field control.";

				throw new BindingException(message);
			}

			return binding;
		}

		private OutputFieldBinding GetOutputFieldBinding(Type type, string? location = null)
		{
			var componentType = type.IsArray
				? typeof(Array)
				: type.IsConstructedGenericType && !type.IsNullabble()
					? type.GetGenericTypeDefinition()
					: type;

			if (!this.outputFieldMetadataMap.TryGetValue(componentType, out OutputFieldBinding binding))
			{
				var message = !string.IsNullOrWhiteSpace(location)
					? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any output field control."
					: $"Type '{type.FullName}' is not bound to any output field control.";

				throw new BindingException(message);
			}

			return binding;
		}
	}
}