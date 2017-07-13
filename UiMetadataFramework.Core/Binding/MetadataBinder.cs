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
		public const string ObjectListOutputControlName = "table";
		public const string ValueListOutputControlName = "list";

		private readonly ConcurrentDictionary<Type, InputFieldBinding> inputFieldMetadataMap = new ConcurrentDictionary<Type, InputFieldBinding>();
		private readonly ConcurrentDictionary<Type, OutputFieldBinding> outputFieldMetadataMap = new ConcurrentDictionary<Type, OutputFieldBinding>();

		public void AddBinding(OutputFieldBinding binding)
		{
			var existingBinding = this.outputFieldMetadataMap.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);
			if (existingBinding != null)
			{
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

		public void AddBinding(InputFieldBinding binding)
		{
			var existingBinding = this.inputFieldMetadataMap.Values.FirstOrDefault(t => t.ClientType == binding.ClientType);
			if (existingBinding != null)
			{
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

		public void AddInputFieldBinding<TServerType>(string clientType)
		{
			this.AddBinding(new InputFieldBinding(typeof(TServerType), clientType));
		}

		public void AddOutputFieldBinding<TServerType>(string clientType)
		{
			this.AddBinding(new OutputFieldBinding(typeof(TServerType), clientType));
		}

		public IEnumerable<InputFieldMetadata> BindInputFields<T>()
		{
			return this.BindInputFields(typeof(T));
		}

		public IEnumerable<InputFieldMetadata> BindInputFields(Type type)
		{
			var properties = type.GetFields();

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
				var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

				var required = propertyType.GetTypeInfo().IsValueType
					// non-nullable value types are automatically required,
					// nullable types are automatically NOT required.
					? Nullable.GetUnderlyingType(propertyType) == null
					// reference types use attribute
					: attribute?.Required ?? false;

				var metadata = new InputFieldMetadata(binding.ClientType)
				{
					Id = property.Name,
					Hidden = attribute?.Hidden ?? false,
					Label = attribute?.Label ?? property.Name,
					OrderIndex = attribute?.OrderIndex ?? 0,
					Required = required,
					DefaultValue = defaultValueAttribute?.AsInputFieldSource(),
					CustomProperties = binding.GetCustomProperties(attribute, property)
				};

				yield return metadata;
			}
		}

		public IEnumerable<OutputFieldMetadata> BindOutputFields(Type type)
		{
			var properties = type.GetFields();

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
						Customizations = attribute?.GetCustomProperties()
					};
				}
				else
				{
					customProperties = attribute?.GetCustomProperties();
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
			var outputFieldBindings = assembly.ExportedTypes
				.Where(t =>
				{
					var typeInfo = t.GetTypeInfo();
					return typeInfo.IsClass &&
						!typeInfo.IsAbstract &&
						typeInfo.IsSubclassOf(typeof(OutputFieldBinding));
				})
				.Select(Activator.CreateInstance)
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
				.Select(Activator.CreateInstance)
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