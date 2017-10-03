namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// This class helps holds mapping between server-side types and client-side UI controls. 
	/// It provides a number of APIs to simplify creation of metadata.
	/// </summary>
	public class MetadataBinder
	{
		/// <summary>
		/// Name of the client-side control which should be able to render tablular data.
		/// </summary>
		public const string ObjectListOutputControlName = "table";

		/// <summary>
		/// Name of the client-side control which should be able to render collection of items as a bullet-point list or similar.
		/// </summary>
		public const string ValueListOutputControlName = "list";

		private readonly DependencyInjectionContainer dependencyInjectionContainer;

		private readonly ConcurrentDictionary<Type, InputFieldBinding> inputFieldMetadataMap = new ConcurrentDictionary<Type, InputFieldBinding>();
		private readonly object key = new object();
		private readonly ConcurrentDictionary<Type, OutputFieldBinding> outputFieldMetadataMap = new ConcurrentDictionary<Type, OutputFieldBinding>();
		private readonly List<string> registeredAssemblies = new List<string>();

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
		public MetadataBinder(DependencyInjectionContainer dependencyInjectionContainer)
		{
			this.dependencyInjectionContainer = dependencyInjectionContainer;
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

			if (binding.ClientType == ObjectListOutputControlName ||
				binding.ClientType == ValueListOutputControlName)
			{
				throw new BindingException(
					$"Binding '{binding.GetType().FullName}' attempts to bind to built-in " +
					$"client type '{binding.ClientType}', which is not allowed.");
			}

			foreach (var serverType in binding.ServerTypes)
			{
				if (this.outputFieldMetadataMap.ContainsKey(serverType))
				{
					throw new InvalidOperationException($"Type '{binding.ServerTypes}' is already bound to output field control '{binding.ClientType}'.");
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
					throw new InvalidOperationException($"Type '{binding.ServerTypes}' is already bound to input field control '{binding.ClientType}'.");
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
		/// <typeparam name="TServerType">Type to bind to a specific client control.</typeparam>
		public void AddOutputFieldBinding<TServerType>(string clientType)
		{
			this.AddBinding(new OutputFieldBinding(typeof(TServerType), clientType));
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as input field(s).</typeparam>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BindInputFields<T>()
		{
			return this.BindInputFields(typeof(T));
		}

		/// <summary>
		/// Retrieves input field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as input field(s).</param>
		/// <returns>List of input field metadata.</returns>
		public IEnumerable<InputFieldMetadata> BindInputFields(Type type)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType.IsConstructedGenericType && !IsNullabble(property)
					? property.PropertyType.GetGenericTypeDefinition()
					: property.PropertyType;

				if (!this.inputFieldMetadataMap.TryGetValue(propertyType, out InputFieldBinding binding))
				{
					throw new KeyNotFoundException($"Type '{propertyType.FullName}' is not bound to any input field control.");
				}

				var attribute = property.GetCustomAttribute<InputFieldAttribute>();
				var inputFieldProcessorAttributes = property.GetCustomAttributes<InputFieldProcessorAttribute>();

				var required = propertyType.GetTypeInfo().IsValueType
					// non-nullable value types are automatically required,
					// nullable types are automatically NOT required.
					? Nullable.GetUnderlyingType(propertyType) == null
					// reference types use attribute
					: attribute?.Required ?? false;

				if (binding.IsInputAlwaysHidden && attribute?.Hidden == false)
				{
					throw new BindingException(
						$"Input '{property.DeclaringType.FullName}.{property.Name}' cannot have `Hidden = true`, " +
						$"because '{propertyType.FullName}' inputs are preconfigured by '{binding.GetType().FullName}' to always be hidden.");
				}

				var metadata = new InputFieldMetadata(binding.ClientType)
				{
					Id = property.Name,
					Hidden = binding.IsInputAlwaysHidden || (attribute?.Hidden ?? false),
					Label = attribute?.Label ?? property.Name,
					OrderIndex = attribute?.OrderIndex ?? 0,
					Required = required,
					Processors = inputFieldProcessorAttributes.Select(t => t.ToMetadata(property, this)).ToList(),
					CustomProperties = binding.GetCustomProperties(attribute, property)
				};

				yield return metadata;
			}
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <param name="type">Type which should be rendered on the client as output field(s).</param>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BindOutputFields(Type type)
		{
			var properties = type.GetPublicProperties();

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType.IsConstructedGenericType && !IsNullabble(property)
					? property.PropertyType.GetGenericTypeDefinition()
					: property.PropertyType;

				this.outputFieldMetadataMap.TryGetValue(propertyType, out OutputFieldBinding binding);

				bool isEnumerable = IsEnumerable(property);

				if (!isEnumerable && binding == null)
				{
					throw new KeyNotFoundException($"Type '{property.PropertyType.FullName}' is not bound to any output field control.");
				}

				object customProperties;
				var attribute = property.GetCustomAttribute<OutputFieldAttribute>();

				var clientControlName = isEnumerable
					? (IsSimpleType(property.PropertyType.GenericTypeArguments[0])
						? ValueListOutputControlName
						: ObjectListOutputControlName)
					: binding.ClientType;

				if (clientControlName == ObjectListOutputControlName)
				{
					customProperties = new EnumerableOutputFieldProperties
					{
						Columns = this.BindOutputFields(property.PropertyType.GenericTypeArguments[0]).ToList(),
						Customizations = attribute?.GetCustomProperties(property, this)
					};
				}
				else
				{
					customProperties = binding != null
						// All non-enumerable properties (i.e. - custom properties) will
						// have binding.
						? binding.GetCustomProperties(property, attribute, this)
						// Only for "ValueListOutputControlName" (i.e. - string[], int[], etc) 
						// will the code come here. 
						: attribute?.GetCustomProperties(property, this);
				}

				var metadata = new OutputFieldMetadata(clientControlName)
				{
					Id = property.Name,
					Hidden = attribute?.Hidden ?? false,
					Label = attribute?.Label ?? property.Name,
					OrderIndex = attribute?.OrderIndex ?? 0,
					CustomProperties = customProperties
				};

				yield return metadata;
			}
		}

		/// <summary>
		/// Retrieves output field metadata for the given type.
		/// </summary>
		/// <typeparam name="T">Type which should be rendered on the client as output field(s).</typeparam>
		/// <returns>List of output field metadata.</returns>
		public IEnumerable<OutputFieldMetadata> BindOutputFields<T>()
		{
			return this.BindOutputFields(typeof(T));
		}

		/// <summary>
		/// Scans assembly for implementations of <see cref="OutputFieldBinding"/> and registers them in this instance of <see cref="MetadataBinder"/>.
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
				.Where(t =>
				{
					var typeInfo = t.GetTypeInfo();
					return typeInfo.IsClass &&
						!typeInfo.IsAbstract &&
						typeInfo.IsSubclassOf(typeof(OutputFieldBinding));
				})
				.Select(t => this.dependencyInjectionContainer.GetInstance(t))
				.Cast<OutputFieldBinding>();

			foreach (var binding in outputFieldBindings)
			{
				this.AddBinding(binding);
			}

			assembly.ExportedTypes.ForEach(t =>
			{
				var attribute = t.GetTypeInfo().GetCustomAttribute<OutputFieldTypeAttribute>();

				if (attribute != null)
				{
					this.AddBinding(new OutputFieldBinding(t, attribute.ClientType));
				}
			});

			var inputFieldBindings = assembly.ExportedTypes
				.Where(t =>
				{
					var typeInfo = t.GetTypeInfo();
					return typeInfo.IsClass &&
						!typeInfo.IsAbstract &&
						typeInfo.IsSubclassOf(typeof(InputFieldBinding));
				})
				.Select(t => this.dependencyInjectionContainer.GetInstance(t))
				.Cast<InputFieldBinding>();

			foreach (var binding in inputFieldBindings)
			{
				this.AddBinding(binding);
			}
		}

		private static bool IsEnumerable(PropertyInfo propertyInfo)
		{
			return
				propertyInfo.PropertyType.GetTypeInfo().IsGenericType &&
				propertyInfo.PropertyType.GetGenericTypeDefinition().GetTypeInfo().GetInterfaces().Any(t => t == typeof(System.Collections.IEnumerable));
		}

		private static bool IsNullabble(PropertyInfo propertyInfo)
		{
			return Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null;
		}

		private static bool IsSimpleType(Type itemType)
		{
			return itemType == typeof(string) || itemType.GetTypeInfo().IsValueType;
		}
	}
}