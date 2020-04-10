namespace UiMetadataFramework.Core.Binding
{
	using System;
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
		/// <returns>Dictionary with custom properties or null, if no <see cref="ICustomPropertyAttribute"/>
		/// were found.</returns>
		public static IDictionary<string, object> GetCustomProperties(this Type type)
		{
			return GetCustomProperties(
				type.GetCustomAttributesImplementingInterface<ICustomPropertyAttribute>(),
				type.FullName);
		}

		/// <summary>
		/// Scans for attributes implementing <see cref="ICustomPropertyAttribute"/> and builds a
		/// dictionary from the collected data.
		/// </summary>
		/// <param name="propertyInfo">Property to scan for <see cref="ICustomPropertyAttribute"/>.</param>
		/// <returns>Dictionary with custom properties or null, if no <see cref="ICustomPropertyAttribute"/>
		/// were found.</returns>
		public static IDictionary<string, object> GetCustomProperties(this PropertyInfo propertyInfo)
		{
			return GetCustomProperties(
				propertyInfo.GetCustomAttributesImplementingInterface<ICustomPropertyAttribute>(),
				propertyInfo.DeclaringType.FullName + "." + propertyInfo.Name);
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
			var formAttribute = formType.GetTypeInfo().GetCustomAttribute<FormAttribute>();

			if (formAttribute == null)
			{
				throw new ArgumentException(
					$"Type '{formType.FullName}' is not a form, " +
					$"because it does not have '{nameof(FormAttribute)}' applied to it.");
			}

			return !string.IsNullOrWhiteSpace(formAttribute.Id)
				? formAttribute.Id
				: formType.FullName;
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
		public static IDictionary<string, T> Merge<T>(this IDictionary<string, T> dictionary, IDictionary<string, T> another)
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
		public static IDictionary<string, T> Set<T>(this IDictionary<string, T> dictionary, string key, T value)
		{
			var result = dictionary.Copy() ?? new Dictionary<string, T>();

			if (result.ContainsKey(key))
			{
				result[key] = value;
			}
			else
			{
				result.Add(key, value);
			}

			return result;
		}

		internal static IReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
		{
			return (IReadOnlyDictionary<TKey, TValue>)dictionary;
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

		internal static T GetCustomAttributeSingleOrDefault<T>(this TypeInfo typeInfo) where T : Attribute
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

		internal static T GetCustomAttributeSingleOrDefault<T>(this PropertyInfo propertyInfo) where T : Attribute
		{
			try
			{
				return propertyInfo.GetCustomAttribute<T>();
			}
			catch (AmbiguousMatchException)
			{
				throw new BindingException(
					$"Property '{propertyInfo.DeclaringType.FullName}.{propertyInfo.Name}' is decorated with multiple attributes of type " +
					$"'{typeof(T).FullName}'. Only one instance of the attribute is allowed.");
			}
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
		private static IDictionary<string, T> Copy<T>(this IDictionary<string, T> dictionary)
		{
			if (dictionary != null)
			{
				return new Dictionary<string, T>(dictionary);
			}

			return null;
		}

		private static IDictionary<string, object> GetCustomProperties(
			IEnumerable<ICustomPropertyAttribute> attributes,
			string nameOfTheDecoratedElement)
		{
			var customPropertyAttributes = attributes
				.Select(t => new
				{
					Attribute = t,
					Usage = t.GetType().GetTypeInfo().GetCustomAttribute<CustomPropertyConfigAttribute>()
				})
				.ToList();

			IDictionary<string, object> result = null;

			var singleValueCustomProperties = customPropertyAttributes
				.Where(t => t.Usage == null || t.Usage.IsArray == false)
				.ToList();

			foreach (var customProperty in singleValueCustomProperties)
			{
				if (result?.ContainsKey(customProperty.Attribute.Name) == true)
				{
					throw new BindingException(
						$"Invalid attempt to add multiple values for custom property '{customProperty.Attribute.Name}' " +
						$"on '{nameOfTheDecoratedElement}'. To allow having multiple values for the custom property " +
						$"'{customProperty.Attribute.Name}', please decorate attribue '{customProperty.Attribute.GetType().FullName}' " +
						$"with '{nameof(CustomPropertyConfigAttribute)}' and set " +
						$"'{nameof(CustomPropertyConfigAttribute)}.{nameof(CustomPropertyConfigAttribute.IsArray)}' to true.");
				}

				result = result.Set(customProperty.Attribute.Name, customProperty.Attribute.GetValue());
			}

			var multiValueCustomProperties = customPropertyAttributes
				.Where(t => t.Usage?.IsArray == true)
				.GroupBy(t => t.Attribute.Name)
				.ToList();

			foreach (var customProperty in multiValueCustomProperties)
			{
				var values = customProperty.Select(t => t.Attribute.GetValue()).ToList();
				result = result.Set(customProperty.Key, values);
			}

			return result;
		}
	}
}