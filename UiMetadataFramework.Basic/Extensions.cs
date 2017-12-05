namespace UiMetadataFramework.Basic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// Provides useful extension methods for working with <see cref="UiMetadataFramework.Basic"/>.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Sets value for a specific key inside the dictionary. If key previously
		/// didn't exist, then it's added.
		/// </summary>
		/// <typeparam name="T">Type of the items stored in the dictionary.</typeparam>
		/// <param name="dictionary">Instance of <see cref="IDictionary{TKey,TValue}"/> or null.</param>
		/// <param name="key">Key whose value to set.</param>
		/// <param name="value">Value to set to.</param>
		/// <returns>Dictionary with the new value in it. Might be same instance as <paramref name="dictionary"/> 
		/// (if <paramref name="dictionary"/> is not null).</returns>
		public static IDictionary<string, T> Set<T>(this IDictionary<string, T> dictionary, string key, T value)
		{
			var result = dictionary ?? new Dictionary<string, T>();

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
	}
}