namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Useful extension methods for working with UI Metadata Framework.
	/// </summary>
	public static class Extensions
	{
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
	}
}