namespace UiMetadataFramework.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public static class Extensions
	{
		public static IDictionary<string, object?>? ConfigAsDictionary(this Component component)
		{
			return component.Configuration as IDictionary<string, object?>;
		}

		public static Component BuildInputComponent<T>(
			this MetadataBinder binder,
			Expression<Func<T, object?>> propertyExpression)
		{
			var propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
			var property = typeof(T).GetProperty(propertyName) ?? throw new Exception($"Property '{propertyName}' not found.");

			return binder.Inputs.BuildComponent(property);
		}

		public static Component BuildOutputComponent<T>(
			this MetadataBinder binder,
			Expression<Func<T, object?>> propertyExpression)
		{
			var propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
			var property = typeof(T).GetProperty(propertyName) ?? throw new Exception($"Property '{propertyName}' not found.");

			return binder.Outputs.BuildComponent(
				property.PropertyType,
				property.GetCustomAttributes<ComponentConfigurationAttribute>(inherit: true).ToArray());
		}

		public static Dictionary<string, object?> ToDictionary(this object? request)
		{
			if (request == null)
			{
				return new Dictionary<string, object?>();
			}

			return request.GetType()
				.GetProperties()
				.Where(t => t is { CanRead: true, MemberType: MemberTypes.Property })
				.Where(t => t.GetMethod!.IsPublic)
				.ToDictionary(t => t.Name, t => t.GetValue(request));
		}
	}
}