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

		private readonly ConcurrentDictionary<Type, InputComponentBinding> inputBindings = new();
		private readonly ConcurrentDictionary<Type, IEnumerable<InputFieldMetadata>> inputFieldCache = new();
		private readonly object key = new();
		private readonly ConcurrentDictionary<Type, OutputComponentBinding> outputBindings = new();
		private readonly ConcurrentDictionary<Type, IEnumerable<OutputFieldMetadata>> outputFieldCache = new();
		private readonly List<string> registeredAssemblies = new();

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures
		/// <see cref="DependencyInjectionContainer.Default"/> to be responsible for instantiating
		/// <see cref="InputComponentBinding"/> and <see cref="OutputComponentBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder()
			: this(DependencyInjectionContainer.Default)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MetadataBinder"/> class and configures the given
		/// <see cref="DependencyInjectionContainer"/> to be responsible for instantiating
		/// <see cref="InputComponentBinding"/> and <see cref="OutputComponentBinding"/> when registering
		/// a new assembly.
		/// </summary>
		public MetadataBinder(IServiceProvider container)
		{
			this.Container = container;
		}

		/// <summary>
		/// Gets list of all registered <see cref="InputComponentBinding"/>.
		/// </summary>
		public IReadOnlyDictionary<Type, InputComponentBinding> InputBindings => this.inputBindings.AsReadOnlyDictionary();

		/// <summary>
		/// Gets list of all registered <see cref="OutputComponentBinding"/>.
		/// </summary>
		public IReadOnlyDictionary<Type, OutputComponentBinding> OutputBindings => this.outputBindings.AsReadOnlyDictionary();

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
		/// Registers given <see cref="OutputComponentBinding"/> instance.
		/// </summary>
		/// <param name="binding"><see cref="OutputComponentBinding"/> instance.</param>
		public void AddBinding(OutputComponentBinding binding)
		{
			var existingBinding = this.outputBindings.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);

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
				if (this.outputBindings.ContainsKey(serverType))
				{
					throw new InvalidOperationException(
						$"Type '{binding.ServerTypes}' is already bound to output field control '{binding.ClientType}'.");
				}

				if (serverType.GetTypeInfo().IsValueType)
				{
					// Bind nullable version of the value type.
					// For example when binding "int", also bind "int?".
					var nullable = typeof(Nullable<>).MakeGenericType(serverType);

					this.outputBindings.TryAdd(nullable, binding);
				}

				this.outputBindings.TryAdd(serverType, binding);
			}
		}

		/// <summary>
		/// Registers given <see cref="InputComponentBinding"/> instance.
		/// </summary>
		/// <param name="binding"><see cref="InputComponentBinding"/> instance.</param>
		public void AddBinding(InputComponentBinding binding)
		{
			var existingBinding = this.inputBindings.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);

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
				if (this.inputBindings.ContainsKey(serverType))
				{
					throw new InvalidOperationException(
						$"Type '{binding.ServerTypes}' is already bound to input field control '{binding.ClientType}'.");
				}

				if (serverType.GetTypeInfo().IsValueType)
				{
					// Bind nullable version of the value type.
					// For example when binding "int", also bind "int?".
					var nullable = typeof(Nullable<>).MakeGenericType(serverType);

					this.inputBindings.TryAdd(nullable, binding);
				}

				this.inputBindings.TryAdd(serverType, binding);
			}
		}

		/// <summary>
		/// Binds specified "server-side" type to the specified "client-side" input control type.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the output field.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		/// <typeparam name="TServerType">Type to bind to a specific client control.</typeparam>
		public void AddInputBinding<TServerType>(string clientType, Type? metadataFactory)
		{
			var binding = new InputComponentBinding(
				typeof(TServerType),
				clientType,
				metadataFactory);

			this.AddBinding(binding);
		}

		/// <summary>
		/// Binds specified "server-side" type to the specified "client-side" output control type.
		/// </summary>
		/// <param name="clientType">Name of the client control which will render the output field.</param>
		/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
		/// be used to construct custom metadata. If null, then no custom metadata will be constructed for
		/// this component.</param>
		/// <typeparam name="TServerType">Type to bind to a specific client control.</typeparam>
		public void AddOutputBinding<TServerType>(
			string clientType,
			Type? metadataFactory)
		{
			var binding = new OutputComponentBinding(
				typeof(TServerType),
				clientType,
				metadataFactory);

			this.AddBinding(binding);
		}

		/// <summary>
		/// Builds metadata for the given input component with the provided list of custom properties. 
		/// </summary>
		/// <param name="type">Type of output component or a <see cref="IPreConfiguredComponent{T}"/>.</param>
		/// <param name="additionalConfigurations">Additional configurations to use when constructing the metadata.</param>
		/// <returns>Metadata for component of type <paramref name="type"/>.</returns>
		/// <exception cref="BindingException">Thrown if a mandatory custom property is missing.</exception>
		public Component BindInputComponent(
			Type type,
			params ConfigurationDataAttribute[] additionalConfigurations)
		{
			var binding = this.GetInputBinding(type);

			return this.BuildComponent(
				type,
				additionalConfigurations,
				binding);
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
		public FormMetadata BuildForm<TForm, TRequest, TResponse>()
		{
			return this.BuildForm(typeof(TForm), typeof(TRequest), typeof(TResponse));
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
		public FormMetadata BuildForm(
			Type formType,
			Type requestType,
			Type responseType)
		{
			return new FormMetadata(this, formType, requestType, responseType);
		}

		/// <summary>
		/// Builds metadata for the given property with an input component. 
		/// </summary>
		public Component BuildInputComponent(PropertyInfo property)
		{
			return this.BindInputComponent(
				property.PropertyType,
				property.GetCustomAttributes<ConfigurationDataAttribute>(inherit: true).ToArray());
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as input field(s).</typeparam>
		/// <param name="strict">If true, then only properties that have <see cref="InputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BuildInputFields<T>(bool strict = false)
		{
			return this.BuildInputFields(typeof(T), strict);
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as input field(s).</param>
		/// <param name="strict">If true, then only properties that have <see cref="InputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BuildInputFields(Type type, bool strict = false)
		{
			return this.inputFieldCache.GetOrAdd(
				type,
				t => this.BuildInputFieldsInternal(t, strict));
		}

		/// <summary>
		/// Builds metadata for the given output component with the provided list of custom properties. 
		/// </summary>
		/// <param name="type">Type of output component or a <see cref="IPreConfiguredComponent{T}"/>.</param>
		/// <param name="additionalConfigurations">Additional configurations to use when constructing the metadata.</param>
		/// <returns>Metadata for component of type <paramref name="type"/>.</returns>
		/// <exception cref="BindingException">Thrown if a mandatory custom property is missing.</exception>
		public Component BuildOutputComponent(
			Type type,
			params ConfigurationDataAttribute[] additionalConfigurations)
		{
			var binding = this.GetOutputBinding(type);

			return this.BuildComponent(
				type,
				additionalConfigurations,
				binding);
		}

		/// <summary>
		/// Builds metadata for the given property with an output component. 
		/// </summary>
		public Component BuildOutputComponent(PropertyInfo property)
		{
			return this.BuildOutputComponent(
				property.PropertyType,
				property.GetCustomAttributes<ConfigurationDataAttribute>(inherit: true).ToArray());
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as output field(s).</param>
		/// <param name="strict">If true, then only properties that have <see cref="OutputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BuildOutputFields(Type type, bool strict = false)
		{
			return this.outputFieldCache.GetOrAdd(
				type,
				t => this.BuildOutputFieldsInternal(t, strict));
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as output field(s).</typeparam>
		/// <param name="strict">If true, then only properties that have <see cref="OutputFieldAttribute"/>
		/// will be taken into account.</param>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BuildOutputFields<T>(bool strict = false)
		{
			return this.BuildOutputFields(typeof(T), strict);
		}

		/// <summary>
		/// Scans assembly for implementations of <see cref="OutputComponentBinding"/>, <see cref="InputComponentBinding"/>
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
							typeInfo.IsSubclassOf(typeof(OutputComponentBinding));
					})
				.Select(t => this.Container.GetService(t))
				.Cast<OutputComponentBinding>();

			foreach (var binding in outputFieldBindings)
			{
				this.AddBinding(binding);
			}

			assembly.ExportedTypes.ForEach(
				t =>
				{
					var attribute = t.GetTypeInfo().GetCustomAttributeSingleOrDefault<OutputComponentAttribute>();

					if (attribute != null)
					{
						this.AddBinding(new OutputComponentBinding(t, attribute));
					}
				});

			var inputFieldBindings = assembly.ExportedTypes
				.Where(
					t =>
					{
						var typeInfo = t.GetTypeInfo();
						return typeInfo.IsClass &&
							!typeInfo.IsAbstract &&
							typeInfo.IsSubclassOf(typeof(InputComponentBinding));
					})
				.Select(t => this.Container.GetService(t))
				.Cast<InputComponentBinding>();

			foreach (var binding in inputFieldBindings)
			{
				this.AddBinding(binding);
			}

			assembly.ExportedTypes.ForEach(
				t =>
				{
					var attribute = t.GetTypeInfo().GetCustomAttributeSingleOrDefault<InputComponentAttribute>();

					if (attribute != null)
					{
						this.AddBinding(new InputComponentBinding(t, attribute));
					}
				});
		}

		internal static string GetFormId(Type formType, FormAttribute formAttribute)
		{
			return !string.IsNullOrWhiteSpace(formAttribute.Id)
				? formAttribute.Id!
				: formType.FullName ?? throw new BindingException($"Cannot form ID for type `{formType}`.");
		}

		internal static PropertyInfo? GetInnerComponent(Type type)
		{
			if (type.ImplementsType(typeof(IPreConfiguredComponent<>)))
			{
				var property = type.GetProperty(nameof(IPreConfiguredComponent<object>.Value))!;

				if (property.PropertyType.ImplementsType(typeof(IPreConfiguredComponent<>)))
				{
					throw new BindingException(
						$"Pre-configured component '{type.FullName}' cannot have nested pre-configured " +
						$"component '{property.PropertyType.FullName}'. Nesting pre-configured components " +
						$"is not supported.");
				}

				return property;
			}

			return null;
		}

		private static Type? GetInnerComponentType(Type type)
		{
			var innerType = type.GetInterfaces(typeof(IPreConfiguredComponent<>))
				.SingleOrDefault()
				?.GenericTypeArguments[0];

			if (innerType == null)
			{
				return null;
			}

			if (innerType == type)
			{
				throw new BindingException(
					$"Type '{type.FullName}' implements '{typeof(IPreConfiguredComponent<>).FullName}' " +
					$"in a recursive way which is invalid.");
			}

			var recursiveInnerType = GetInnerComponentType(innerType);

			if (recursiveInnerType != null)
			{
				throw new BindingException(
					$"Type '{type.FullName}' implements '{typeof(IPreConfiguredComponent<>).FullName}' " +
					$"with nested pre-configured component '{innerType.FullName}'. Nesting pre-configured " +
					$"components is not supported.");
			}

			return innerType;
		}

		/// <summary>
		/// Builds component metadata.
		/// </summary>
		/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
		/// <param name="configurationItems">Any additional configuration items to apply.</param>
		/// <param name="binding">Component's binding.</param>
		/// <returns><see cref="Component"/> instance.</returns>
		/// <exception cref="BindingException">Thrown if the supplied configuration data is invalid.</exception>
		private Component BuildComponent(
			Type type,
			ConfigurationDataAttribute[] configurationItems,
			IFieldBinding binding)
		{
			var effectiveConfigurationItems = configurationItems;

			var innerComponent = GetInnerComponent(type);

			if (innerComponent != null)
			{
				effectiveConfigurationItems = configurationItems
					// Inner configuration items should come last. This way we indicate
					// that inner configuration items have lower priority.
					.Concat(innerComponent.GetCustomAttributes<ConfigurationDataAttribute>(true))
					.ToArray();
			}

			var metadataFactory = binding.MetadataFactory != null
				? (IMetadataFactory)this.Container.GetService(binding.MetadataFactory)
				: new DefaultMetadataFactory();

			try
			{
				var metadata = metadataFactory.CreateMetadata(
					type,
					this,
					effectiveConfigurationItems);

				return new Component(
					binding.ClientType,
					metadata);
			}
			catch (Exception e)
			{
				throw new BindingException($"Failed to construct metadata for '{type.FullName}'.", e);
			}
		}

		private IEnumerable<InputFieldMetadata> BuildInputFieldsInternal(Type type, bool strict = false)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttributeSingleOrDefault<InputFieldAttribute>();

				if (strict && attribute == null)
				{
					continue;
				}

				var binding = this.GetInputBinding(
					property.PropertyType,
					$"{property.DeclaringType?.FullName}.{property.Name}");

				attribute ??= new InputFieldAttribute
				{
					Required = false,
					Hidden = false
				};

				var metadata = attribute.GetMetadata(property, binding, this);

				yield return metadata;
			}
		}

		private IEnumerable<OutputFieldMetadata> BuildOutputFieldsInternal(Type type, bool strict = false)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttributeSingleOrDefault<OutputFieldAttribute>();

				if (strict && attribute == null)
				{
					continue;
				}

				var binding = this.GetOutputBinding(
					property.PropertyType,
					$"{property.DeclaringType}.{property.Name}");

				attribute ??= new OutputFieldAttribute();

				yield return attribute.GetMetadata(property, binding, this);
			}
		}

		private InputComponentBinding GetInputBinding(Type type, string? location = null)
		{
			var binding = this.GetInputBindingOrNull(type);

			if (binding == null)
			{
				var message = !string.IsNullOrWhiteSpace(location)
					? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any input field control."
					: $"Type '{type.FullName}' is not bound to any input field control.";

				throw new BindingException(message);
			}

			return binding;
		}

		private InputComponentBinding? GetInputBindingOrNull(Type type)
		{
			var effectiveType = GetInnerComponentType(type) ?? type;

			var componentType = effectiveType.IsConstructedGenericType && !effectiveType.IsNullabble()
				? effectiveType.GetGenericTypeDefinition()
				: effectiveType;

			this.inputBindings.TryGetValue(componentType, out InputComponentBinding binding);

			return binding;
		}

		private OutputComponentBinding GetOutputBinding(Type type, string? location = null)
		{
			var binding = this.GetOutputBindingOrNull(type);

			if (binding == null)
			{
				var message = !string.IsNullOrWhiteSpace(location)
					? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any output field control."
					: $"Type '{type.FullName}' is not bound to any output field control.";

				throw new BindingException(message);
			}

			return binding;
		}

		private OutputComponentBinding? GetOutputBindingOrNull(Type type)
		{
			var effectiveType = GetInnerComponentType(type) ?? type;

			var componentType = effectiveType.IsArray
				? typeof(Array)
				: effectiveType.IsConstructedGenericType && !effectiveType.IsNullabble()
					? effectiveType.GetGenericTypeDefinition()
					: effectiveType;

			this.outputBindings.TryGetValue(componentType, out OutputComponentBinding binding);

			return binding;
		}
	}
}