namespace UiMetadataFramework.Core;

using System;

internal static class InternalExtensions
{
	/// <summary>
	/// Checks whether this class inherits another class.
	/// </summary>
	/// <param name="type">Type which might be inheriting from the other class.</param>
	/// <param name="baseClass">Base class which should be implemented by <paramref name="type"/>.</param>
	/// <returns>True or false.</returns>
	public static bool ImplementsClass(this Type type, Type baseClass)
	{
		return type.GetBaseClassOfType(baseClass) != null;
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
}