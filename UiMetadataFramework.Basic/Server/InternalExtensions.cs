namespace UiMetadataFramework.Basic.Server
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Collection of extension methods from UiMetadataFramework.MediatR.
	/// </summary>
	internal static class InternalExtensions
	{
		public static T? GetCustomAttributeSingleOrDefault<T>(this TypeInfo typeInfo, bool inherit = true) where T : Attribute
		{
			try
			{
				return typeInfo.GetCustomAttribute<T>(inherit);
			}
			catch (AmbiguousMatchException)
			{
				throw new BindingException(
					$"Type '{typeInfo.FullName}' is decorated with multiple attributes of type " +
					$"'{typeof(T).FullName}'. Only one instance of the attribute is allowed.");
			}
		}

		internal static IEnumerable<Type> GetInterfaces(this Type type, Type toFind)
		{
			if (toFind.GetTypeInfo().IsGenericType)
			{
				return type.GetTypeInfo()
					.GetInterfaces()
					.Where(t => t.IsConstructedGenericType && t.GetGenericTypeDefinition() == toFind);
			}

			return type.GetTypeInfo()
				.GetInterfaces()
				.Where(t => t == toFind);
		}

		/// <summary>
		/// Checks whether this class inherits another class.
		/// </summary>
		/// <param name="type">Type which might be inheriting from the other class.</param>
		/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
		/// <returns>True or false.</returns>
		internal static bool ImplementsClass(this Type type, Type baseClass)
		{
			return type.GetBaseClassOfType(baseClass) != null;
		}

		internal static async Task<object> InvokeAsync(
			this MethodInfo @this,
			object obj,
			params object?[] parameters)
		{
			dynamic awaitable = @this.Invoke(obj, parameters);
			await awaitable;
			return awaitable.GetAwaiter().GetResult();
		}

		/// <summary>
		/// Checks whether this class inherits another class.
		/// </summary>
		/// <param name="type">Type which might be inheriting from the other class.</param>
		/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
		/// <returns>Type implementing the <paramref name="baseClass"/>.</returns>
		private static Type? GetBaseClassOfType(this Type type, Type baseClass)
		{
			if (type == baseClass)
			{
				return baseClass;
			}

			var baseType = type.GetTypeInfo().BaseType;

			if (baseType == null)
			{
				return null;
			}

			var baseClassTypeInfo = baseClass.GetTypeInfo();

			// T1 : T2<int>
			if (baseClassTypeInfo.IsGenericType &&
				baseType.IsConstructedGenericType)
			{
				var genericType = baseClass.IsConstructedGenericType
					? baseType
					: baseType.GetGenericTypeDefinition();

				if (genericType == baseClass)
				{
					return baseClassTypeInfo.ContainsGenericParameters
						? baseType.GetGenericTypeDefinition()
						: baseType;
				}
			}

			// T1 : T2
			return baseType.GetBaseClassOfType(baseClass);
		}
		
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
		
		internal static IEnumerable<T> GetCustomAttributesImplementingInterface<T>(this Type type)
		{
			return type
				.GetTypeInfo()
				.GetCustomAttributes()
				.Where(t => typeof(T).GetTypeInfo().IsInstanceOfType(t))
				.Cast<T>();
		}
	}
}