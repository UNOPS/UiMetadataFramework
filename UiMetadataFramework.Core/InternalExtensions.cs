namespace UiMetadataFramework.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

internal static class InternalExtensions
{
	/// <summary>
	/// Retrieves a collection of interfaces implemented by a given type that match a specific interface.
	/// </summary>
	/// <param name="type">The type for which to find the implemented interfaces.</param>
	/// <param name="toFind">The specific interface to find among the implemented interfaces.</param>
	/// <returns>An IEnumerable of Type objects representing the matching interfaces implemented by the input type.</returns>
	/// <remarks>
	/// <list type="bullet">
	/// <item>If the 'toFind' interface is a generic type, this method will return all interfaces of the
	/// input type that are constructed generic types and have the same generic type definition as 'toFind'.</item>
	/// <item>If the 'toFind' interface is not a generic type, this method will return all interfaces of the
	/// input type that are exactly the same as 'toFind'.</item>
	/// </list>
	/// </remarks>
	public static IEnumerable<Type> GetInterfaces(this Type type, Type toFind)
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
	/// <param name="type">Type which might be inheriting <paramref name="baseClass"/>.</param>
	/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
	/// <returns>True or false.</returns>
	public static bool ImplementsClass(this Type type, Type baseClass)
	{
		return type.GetBaseClassOfType(baseClass) != null;
	}

	/// <summary>
	/// Checks whether this type implements the given class/interface.
	/// </summary>
	/// <param name="type">Type which might be implementing <paramref name="baseType"/>.</param>
	/// <param name="baseType">Class or interface. Can be a generic type (both constructed and
	/// non-constructed generic types are supported).</param>
	/// <returns></returns>
	public static bool ImplementsType(this Type type, Type baseType)
	{
		if (baseType.IsInterface)
		{
			return ImplementsInterface(type, baseType);
		}

		return type.GetBaseClassOfType(baseType) != null;
	}

	/// <summary>
	/// Checks whether this class inherits another class.
	/// </summary>
	/// <param name="type">Type which might be inheriting <paramref name="baseClass"/>.</param>
	/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
	/// <returns>Type implementing the <paramref name="baseClass"/>.</returns>
	private static Type? GetBaseClassOfType(this Type type, Type baseClass)
	{
		if (type == baseClass)
		{
			return baseClass;
		}

		if (type.BaseType == null)
		{
			return null;
		}

		// T1 : T2<int>
		if (baseClass.IsGenericType &&
			type.BaseType.IsConstructedGenericType)
		{
			var genericType = baseClass.IsConstructedGenericType
				? type.BaseType
				: type.BaseType.GetGenericTypeDefinition();

			if (genericType == baseClass)
			{
				return type.BaseType.ContainsGenericParameters
					? type.BaseType.GetGenericTypeDefinition()
					: type.BaseType;
			}
		}

		// T1 : T2
		return type.BaseType.GetBaseClassOfType(baseClass);
	}

	/// <summary>
	/// Checks whether this type implements the given interface.
	/// </summary>
	/// <param name="type">Type which might be implementing <paramref name="baseInterface"/>.</param>
	/// <param name="baseInterface">Interface which should be implemented by <paramref name="type"/>.</param>
	/// <returns>True or false.</returns>
	private static bool ImplementsInterface(Type type, Type baseInterface)
	{
		return type.GetInterfaces(baseInterface).Any();
	}
}