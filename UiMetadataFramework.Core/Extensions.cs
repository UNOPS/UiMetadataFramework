namespace UiMetadataFramework.Core
{
	using System;
	using System.Linq.Expressions;
	using System.Reflection;

	internal static class Extensions
	{
		public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
		{
			var type = typeof(TSource);

			var member = propertyLambda.Body as MemberExpression;
			if (member == null)
			{
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");
			}

			var propInfo = member.Member as PropertyInfo;
			if (propInfo == null)
			{
				throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
			}

			if (type != propInfo.PropertyType && !type.GetTypeInfo().IsSubclassOf(propInfo.PropertyType))
			{
				throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");
			}

			return propInfo;
		}

		internal static bool IsNullableEnum(this Type t)
		{
			var u = Nullable.GetUnderlyingType(t);
			return u != null && u.GetTypeInfo().IsEnum;
		}
	}
}