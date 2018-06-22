namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;

	/// <summary>
	/// Collection of extension methods from UiMetadataFramework.MediatR.
	/// </summary>
	internal static class Extensions
	{
		/// <summary>
		/// Checks whether this class inherits another class.
		/// </summary>
		/// <param name="type">Type which might be inheriting from the other class.</param>
		/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
		/// <returns>Type implementing the <paramref name="baseClass"/>.</returns>
		internal static Type GetBaseClassOfType(this Type type, Type baseClass)
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

			if (baseClassTypeInfo.IsGenericType)
			{
				// T1 : T2<int>
				if (baseType.IsConstructedGenericType)
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
			}

			// T1 : T2
			return baseType.GetBaseClassOfType(baseClass);
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

		internal static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
		{
			dynamic awaitable = @this.Invoke(obj, parameters);
			await awaitable;
			return awaitable.GetAwaiter().GetResult();
		}
	}
}