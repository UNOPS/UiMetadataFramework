namespace UiMetadataFramework.Core
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Threading.Tasks;

	internal static class Extensions
	{
		public static string Acronymize(this string value)
		{
			var firstLetterOfEachWord = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t[0]).ToList();
			return string.Join("", firstLetterOfEachWord).ToUpper();
		}

		public static string GetEmbeddedResourceText(this Assembly assembly, string embeddedResourceName)
		{
			using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
			{
				if (stream == null)
				{
					return null;
				}

				using (var ms = new StreamReader(stream))
				{
					return ms.ReadToEnd();
				}
			}
		}

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

		public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
		{
			dynamic awaitable = @this.Invoke(obj, parameters);
			await awaitable;
			return awaitable.GetAwaiter().GetResult();
		}

		internal static bool IsNullableEnum(this Type t)
		{
			var u = Nullable.GetUnderlyingType(t);
			return u != null && u.GetTypeInfo().IsEnum;
		}
	}
}