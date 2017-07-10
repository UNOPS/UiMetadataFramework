namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	internal static class Extensions
	{
		public static IEnumerable<PropertyInfo> GetFields(this Type type)
		{
			return type.GetRuntimeProperties()
				.Where(t => t.GetCustomAttribute<NotFieldAttribute>() == null)
				.ToList();
		}

		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items)
			{
				action(item);
			}
		}
	}
}