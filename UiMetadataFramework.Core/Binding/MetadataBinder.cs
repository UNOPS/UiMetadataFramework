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
		private readonly ConcurrentDictionary<Type, InputFieldBinding> inputFieldMetadataMap = new ConcurrentDictionary<Type, InputFieldBinding>();
		private readonly ConcurrentDictionary<Type, OutputFieldBinding> outputFieldMetadataMap = new ConcurrentDictionary<Type, OutputFieldBinding>();

		public void AddBinding(OutputFieldBinding binding)
		{
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
				var propertyType = property.PropertyType;

				if (!this.inputFieldMetadataMap.TryGetValue(propertyType, out InputFieldBinding binding))
				{
					throw new KeyNotFoundException($"Type '{propertyType.FullName}' is not bound to any input field control.");
				}

				var attribute = property.GetCustomAttribute<InputFieldAttribute>();
				var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

				var metadata = new InputFieldMetadata(binding.ClientType)
				{
					Id = property.Name,
					Hidden = attribute?.Hidden ?? false,
					Label = attribute?.Label ?? property.Name,
					OrderIndex = attribute?.OrderIndex ?? 0,
					Required = attribute?.Required ?? false,
					DefaultValue = defaultValueAttribute?.AsInputFieldSource(),
					CustomProperties = binding.GetCustomProperties(attribute)
				};

				yield return metadata;
			}
		}

		public IEnumerable<OutputFieldMetadata> BindOutputFields(Type type)
		{
			var properties = type.GetFields();

			foreach (var property in properties)
			{
				var propertyType = property.PropertyType;

				if (!this.outputFieldMetadataMap.TryGetValue(propertyType, out OutputFieldBinding binding))
				{
					throw new KeyNotFoundException($"Type '{propertyType.FullName}' is not bound to any output field control.");
				}

				var attribute = property.GetCustomAttribute<OutputFieldAttribute>();

				var metadata = new OutputFieldMetadata(binding.ClientType)
				{
					Id = property.Name,
					Hidden = attribute?.Hidden ?? false,
					Label = attribute?.Label ?? property.Name,
					OrderIndex = attribute?.OrderIndex ?? 0,
					CustomProperties = binding.GetCustomProperties(attribute)
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
	}
}