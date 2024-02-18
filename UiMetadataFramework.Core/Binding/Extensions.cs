namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Useful extension methods for working with UI Metadata Framework.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Scans for attributes implementing <see cref="ICustomPropertyAttribute"/> and builds a
		/// dictionary from the collected data.
		/// </summary>
		/// <param name="type">Type to scan for <see cref="ICustomPropertyAttribute"/>.</param>
		/// <param name="binder">Metadata binder instance.</param>
		/// <returns>Dictionary with custom properties or null, if no <see cref="ICustomPropertyAttribute"/>
		/// were found.</returns>
		public static IDictionary<string, object?>? GetCustomProperties(this Type type, MetadataBinder binder)
		{
			return type
				.GetCustomAttributesImplementingInterface<ICustomPropertyAttribute>()
				.GetCustomProperties(type, type.FullName ?? throw new BindingException($"Cannot get full name of type `{type}`."), binder);
		}

		/// <summary>
		/// Scans for attributes implementing <see cref="ICustomPropertyAttribute"/> and builds a
		/// dictionary from the collected data.
		/// </summary>
		/// <param name="propertyInfo">Property to scan for <see cref="ICustomPropertyAttribute"/>.</param>
		/// <param name="binder">Metadata binder instance.</param>
		/// <returns>Dictionary with custom properties or null, if no <see cref="ICustomPropertyAttribute"/>
		/// were found.</returns>
		public static IDictionary<string, object?>? GetCustomProperties(this PropertyInfo propertyInfo, MetadataBinder binder)
		{
			return propertyInfo
				.GetCustomAttributesImplementingInterface<ICustomPropertyAttribute>()
				.GetCustomProperties(propertyInfo.PropertyType, propertyInfo.DeclaringType!.FullName + "." + propertyInfo.Name, binder);
		}

		/// <summary>
		/// Gets unique identifier for the specified form. If form has <see cref="FormAttribute"/> and
		/// <see cref="FormAttribute.Id"/> is specified, then the <see cref="FormAttribute.Id"/> will be returned.
		/// Otherwise class' full name is returned.
		/// </summary>
		/// <param name="formType">Type representing a form.</param>
		/// <returns>Unique identifier of the form.</returns>
		public static string GetFormId(this Type formType)
		{
			return MetadataBinder.GetFormId(formType);
		}

		/// <summary>
		/// Checks if the property implements <see cref="System.Collections.IEnumerable"/> interface.
		/// </summary>
		/// <param name="propertyInfo">Property to check.</param>
		/// <returns>True/false.</returns>
		public static bool IsEnumerable(this PropertyInfo propertyInfo)
		{
			return
				propertyInfo.PropertyType.GetTypeInfo().IsGenericType &&
				propertyInfo.PropertyType.GetGenericTypeDefinition()
					.GetTypeInfo()
					.GetInterfaces()
					.Any(t => t == typeof(IEnumerable));
		}

		/// <summary>
		/// Checks if the type is nullable.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>True/false.</returns>
		public static bool IsNullabble(this Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		/// <summary>
		/// Merges two dictionaries together. If both dictionaries have the same key, then
		/// items in <paramref name="another"/> dictionary will take precedence.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dictionary">Instance of <see cref="IDictionary{TKey,TValue}"/> or null.</param>
		/// <param name="another">Instance of <see cref="IDictionary{TKey,TValue}"/> or null. 
		/// Values from this dictionary will be added to <paramref name="dictionary"/>.</param>
		/// <returns>New dictionary containing both items from <paramref name="dictionary"/> 
		/// and <paramref name="another"/>.</returns>
		public static IDictionary<string, T?>? Merge<T>(this IDictionary<string, T?>? dictionary, IDictionary<string, T?>? another)
		{
			var result = dictionary.Copy();

			if (another != null)
			{
				foreach (var anotherItem in another)
				{
					result = result.Set(anotherItem.Key, anotherItem.Value);
				}
			}

			return result;
		}

		/// <summary>
		/// Sets value for a specific key inside the dictionary. If key previously
		/// didn't exist, then it's added, otherwise the old value is overwritten.
		/// </summary>
		/// <typeparam name="T">Type of the items stored in the dictionary.</typeparam>
		/// <param name="dictionary">Instance of <see cref="IDictionary{TKey,TValue}"/> or null.</param>
		/// <param name="key">Key whose value to set.</param>
		/// <param name="value">Value to set to.</param>
		/// <returns>New dictionary containing items from <paramref name="dictionary"/> and the new item
		/// added by this method.</returns>
		public static IDictionary<string, T?> Set<T>(
			this IDictionary<string, T?>? dictionary,
			string key,
			T? value)
		{
			var result = dictionary.Copy() ?? new Dictionary<string, T?>();

			result[key] = value;

			return result;
		}

		internal static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
		{
			return dictionary;
		}

		internal static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}

		internal static IEnumerable<T> GetCustomAttributesImplementingInterface<T>(this PropertyInfo propertyInfo)
		{
			return propertyInfo
				.GetCustomAttributes()
				.Where(t => typeof(T).GetTypeInfo().IsInstanceOfType(t))
				.Cast<T>();
		}

		internal static IEnumerable<T> GetCustomAttributesImplementingInterface<T>(this Type type)
		{
			return type
				.GetTypeInfo()
				.GetCustomAttributes()
				.Where(t => typeof(T).GetTypeInfo().IsInstanceOfType(t))
				.Cast<T>();
		}

		internal static T? GetCustomAttributeSingleOrDefault<T>(this TypeInfo typeInfo) where T : Attribute
		{
			try
			{
				return typeInfo.GetCustomAttribute<T>();
			}
			catch (AmbiguousMatchException)
			{
				throw new BindingException(
					$"Type '{typeInfo.FullName}' is decorated with multiple attributes of type " +
					$"'{typeof(T).FullName}'. Only one instance of the attribute is allowed.");
			}
		}

		internal static T? GetCustomAttributeSingleOrDefault<T>(this PropertyInfo propertyInfo, bool inherit = true) where T : Attribute
		{
			try
			{
				return propertyInfo.GetCustomAttribute<T>(inherit);
			}
			catch (AmbiguousMatchException)
			{
				throw new BindingException(
					$"Property '{propertyInfo.DeclaringType!.FullName}.{propertyInfo.Name}' is decorated with multiple attributes of type " +
					$"'{typeof(T).FullName}'. Only one instance of the attribute is allowed.");
			}
		}

		internal static IDictionary<string, object?>? GetCustomProperties(
			this IEnumerable<ICustomPropertyAttribute> attributes,
			Type type,
			string location,
			MetadataBinder binder)
		{
			var customPropertyAttributes = attributes
				.Select(
					t => new
					{
						Attribute = t,
						Usage = t.GetType().GetTypeInfo().GetCustomAttribute<CustomPropertyConfigAttribute>()
					})
				.ToList();

			IDictionary<string, object?>? result = null;

			var singleValueCustomProperties = customPropertyAttributes
				.Where(t => t.Usage == null || t.Usage.IsArray == false)
				.ToList();

			foreach (var customProperty in singleValueCustomProperties)
			{
				if (result?.ContainsKey(customProperty.Attribute.Name) == true)
				{
					throw new BindingException(
						$"Invalid attempt to add multiple values for custom property '{customProperty.Attribute.Name}' " +
						$"on '{location}'. To allow having multiple values for the custom property " +
						$"'{customProperty.Attribute.Name}', please decorate attribue '{customProperty.Attribute.GetType().FullName}' " +
						$"with '{nameof(CustomPropertyConfigAttribute)}' and set " +
						$"'{nameof(CustomPropertyConfigAttribute)}.{nameof(CustomPropertyConfigAttribute.IsArray)}' to true.");
				}

				result = result.Set(customProperty.Attribute.Name, customProperty.Attribute.GetValue(type, binder));
			}

			var multiValueCustomProperties = customPropertyAttributes
				.Where(t => t.Usage?.IsArray == true)
				.GroupBy(t => t.Attribute.Name)
				.ToList();

			foreach (var customProperty in multiValueCustomProperties)
			{
				var values = customProperty.Select(t => t.Attribute.GetValue(type, binder)).ToList();
				result = result!.Set(customProperty.Key, values);
			}

			return result;
		}

		internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type)
		{
			return type.GetRuntimeProperties()
				.Where(t => t.CanRead && t.GetMethod.IsPublic)
				.Where(t => t.GetCustomAttribute<NotFieldAttribute>() == null)
				.ToList();
		}

		/// <summary>
		/// Creates a copy of the dictionary.
		/// </summary>
		private static IDictionary<string, T?>? Copy<T>(this IDictionary<string, T?>? dictionary)
		{
			if (dictionary != null)
			{
				return new Dictionary<string, T?>(dictionary);
			}

			return null;
		}
	}
}