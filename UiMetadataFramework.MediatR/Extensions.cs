namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Reflection;
	using System.Threading.Tasks;

	public static class Extensions
	{
		/// <summary>
		/// Gets unique identifier for the specified form. If form has <see cref="FormAttribute"/> and
		/// <see cref="FormAttribute.Id"/> is specified, then the <see cref="FormAttribute.Id"/> will be returned.
		/// Otherwise class' full name is returned.
		/// </summary>
		/// <param name="formType">Type representing an <see cref="IForm{TRequest,TResponse,TResponseMetadata}"/>.</param>
		/// <returns>Unique identifier of the form.</returns>
		public static string GetFormId(this Type formType)
		{
			var formAttribute = formType.GetTypeInfo().GetCustomAttribute<FormAttribute>();

			return !string.IsNullOrWhiteSpace(formAttribute?.Id)
				? formAttribute.Id
				: formType.FullName;
		}

		internal static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
		{
			dynamic awaitable = @this.Invoke(obj, parameters);
			await awaitable;
			return awaitable.GetAwaiter().GetResult();
		}
	}
}