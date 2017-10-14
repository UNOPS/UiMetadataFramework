namespace UiMetadataFramework.MediatR
{
	using System.Reflection;
	using System.Threading.Tasks;

	/// <summary>
	/// Collection of extension methods from UiMetadataFramework.MediatR.
	/// </summary>
	public static class Extensions
	{
		internal static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
		{
			dynamic awaitable = @this.Invoke(obj, parameters);
			await awaitable;
			return awaitable.GetAwaiter().GetResult();
		}
	}
}