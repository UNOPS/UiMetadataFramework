namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using UiMetadataFramework.Core.Attributes;
	using UiMetadataFramework.Core.Metadata;
	using UiMetadataFramework.Core.UI.Actions;
	using UiMetadataFramework.Core.UI.Outputs;

	/// <summary>
	/// Represents bindings between property metadata and model classes.
	/// </summary>
	public class MetadataBinder
	{
		public static readonly MetadataBinder Default = new MetadataBinder();

		public ConcurrentDictionary<Type, Type> Bindings { get; set; } = new ConcurrentDictionary<Type, Type>();

		public ConcurrentDictionary<Type, IEnumerable<PropertyMetadata>> Cache { get; set; } =
			new ConcurrentDictionary<Type, IEnumerable<PropertyMetadata>>();

		public void Bind(Type model, Type property)
		{
			if (this.Bindings.ContainsKey(model))
			{
				throw new InvalidOperationException($"Model of type {model.Name} is already bound to property of type {property.Name}.");
			}

			if (model.GetTypeInfo().IsValueType)
			{
				// Bind nullable version of the value type.
				// For example when binding "int", also bind "int?".
				var nullable = typeof(Nullable<>).MakeGenericType(model);

				this.Bindings.TryAdd(nullable, property);
			}

			this.Bindings.TryAdd(model, property);
		}

		public void Bind<TModel, TProperty>()
			where TProperty : PropertyMetadata
		{
			this.Bind(typeof(TModel), typeof(TProperty));
		}

		public IEnumerable<PropertyMetadata> BindProperties<T>()
		{
			var containerType = typeof(T);

			if (this.Cache.ContainsKey(containerType))
			{
				return this.Cache[containerType];
			}

			var result = this.BindProperties(null, containerType);
			this.Cache.TryAdd(containerType, result);

			return this.Cache[containerType];
		}

		public ObjectMetadata<TModel> In<TModel>()
		{
			return new ObjectMetadata<TModel>();
		}

		public PropertyMetadata In<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			return this.Property(property);
		}

		public PropertyMetadata Property<TModel, TProperty>(string container, Expression<Func<TModel, TProperty>> property)
		{
			var propertyInfo = property.GetPropertyInfo();
			return this.Property(container, propertyInfo);
		}

		public PropertyMetadata Property<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
		{
			return this.Property(null, property);
		}

		private static string GetPropertyPath(string container, string propertyName)
		{
			var propertyPath = !string.IsNullOrWhiteSpace(container)
				? container + "." + propertyName
				: propertyName;
			return propertyPath;
		}

		private static bool IsEnumerable(PropertyInfo propertyInfo)
		{
			return
				propertyInfo.PropertyType.GetTypeInfo().IsGenericType &&
				propertyInfo.PropertyType.GetGenericTypeDefinition().GetTypeInfo().GetInterfaces().Any(t => t == typeof(System.Collections.IEnumerable));
		}

		private IEnumerable<PropertyMetadata> BindProperties(string container, Type type)
		{
			var result = new List<PropertyMetadata>();

			var properties = type.GetTypeInfo().GetProperties().ToList();

			foreach (var propertyInfo in properties)
			{
				var isEnumerable = IsEnumerable(propertyInfo);

				var modelType = isEnumerable
					? propertyInfo.PropertyType.GenericTypeArguments[0]
					: propertyInfo.PropertyType;

				if (modelType.IsNullableEnum())
				{
					modelType = Nullable.GetUnderlyingType(modelType);
				}

				var uiModelAttribute = modelType.GetTypeInfo().GetCustomAttribute<UiModelAttribute>();

				var hasBinding =
					uiModelAttribute != null ||
					modelType.GetTypeInfo().IsEnum ||
					this.Bindings.ContainsKey(modelType);

				if (hasBinding)
				{
					var uiPropertyAttribute = propertyInfo.GetCustomAttribute<UiPropertyAttribute>();
					var propertyMetadata = this.Property(container, modelType, propertyInfo, uiPropertyAttribute);
					result.Add(propertyMetadata);
				}
				else
				{
					var uiPropertyAttribute = propertyInfo.GetCustomAttribute<UiPropertyAttribute>();
					if (uiPropertyAttribute != null)
					{
						var propertyMetadata = new PropertyMetadata(GetPropertyPath(container, propertyInfo.Name), uiPropertyAttribute.Type)
						{
							OrderIndex = uiPropertyAttribute.OrderIndex,
							IsList = isEnumerable,
							Hidden = uiPropertyAttribute.Hidden,
							Label = uiPropertyAttribute.Label
						};

						result.Add(propertyMetadata);
					}

					// This is the container property, we only need to bind child properties.
					var children = this.BindProperties(GetPropertyPath(container, propertyInfo.Name), modelType);
					result.AddRange(children);
				}
			}

			return result;
		}

		private PropertyMetadata Property(string container, PropertyInfo propertyInfo)
		{
			var modelType = propertyInfo.PropertyType;

			if (IsEnumerable(propertyInfo))
			{
				modelType = propertyInfo.PropertyType.GenericTypeArguments[0];
			}

			var attribute = propertyInfo.GetCustomAttribute<UiPropertyAttribute>();

			return this.Property(container, modelType, propertyInfo, attribute);
		}

		private PropertyMetadata Property(string container, Type modelType, PropertyInfo property, UiPropertyAttribute uiPropertyAttribute)
		{
			var hasBinding = this.Bindings.ContainsKey(modelType);
			var uiModelAttribute = modelType.GetTypeInfo().GetCustomAttribute<UiModelAttribute>();
			var isEnum = modelType.GetTypeInfo().IsEnum;

			if (!hasBinding && uiModelAttribute == null && !isEnum)
			{
				throw new KeyNotFoundException($"Model of type {modelType.Name} was not bound to any property metadata.");
			}

			var propertyPath = GetPropertyPath(container, property.Name);

			PropertyMetadata metadata;

			if (uiModelAttribute != null)
			{
				// If [UiModel] attribute exists on the model class, then we use it,
				// otherwise we look for binding.
				metadata = new PropertyMetadata(propertyPath, uiModelAttribute.Name);
			}
			else if (hasBinding)
			{
				metadata = (PropertyMetadata)Activator.CreateInstance(this.Bindings[modelType], propertyPath);
			}
			else
			{
				metadata = new EnumProperty(propertyPath);
			}

			if (uiPropertyAttribute != null)
			{
				var uiActionAttribute = uiPropertyAttribute as UiActionAttribute;
				var linkAttribute = uiPropertyAttribute as UiLinkAttribute;

				if (uiActionAttribute != null)
				{
					var actionAttribute = uiActionAttribute;

					var formLinkProperty = new FormLinkProperty(actionAttribute.Form, propertyPath)
					{
						AnchorText = actionAttribute.AnchorText ?? propertyPath,
						Target = actionAttribute.Target,
						Parameters = property
							.GetCustomAttributes<UiActionParameterAttribute>()
							.Select(t => new FormParameter(t.Parameter, GetPropertyPath(container, t.Property)))
							.ToArray()
					};

					metadata = formLinkProperty;
				}
				else if (linkAttribute != null)
				{
					metadata = linkAttribute.ToProperty(propertyPath);
				}
				else
				{
					metadata.Parameters = uiPropertyAttribute.Parameters;
				}

				metadata.OrderIndex = uiPropertyAttribute.OrderIndex;
				metadata.Hidden = uiPropertyAttribute.Hidden;
				metadata.Label = uiPropertyAttribute.Label;

				// Override metadata type if needed.
				if (!string.IsNullOrWhiteSpace(uiPropertyAttribute.Type))
				{
					metadata.Type = uiPropertyAttribute.Type;
				}
			}

			metadata.IsList = IsEnumerable(property) && uiModelAttribute?.IsEnumerableModel == false;

			return metadata;
		}
	}
}