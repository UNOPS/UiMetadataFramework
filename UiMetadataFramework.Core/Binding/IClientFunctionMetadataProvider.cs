namespace UiMetadataFramework.Core.Binding
{
	/// <summary>
	/// Represents an object which can give <see cref="ClientFunctionMetadata"/>.
	/// </summary>
	public interface IClientFunctionMetadataProvider
	{
		/// <summary>
		/// Returns metadata describing a client-side function.
		/// </summary>
		/// <returns>Instance of <see cref="ClientFunctionMetadata"/>.</returns>
		ClientFunctionMetadata GetClientFunctionMetadata();
	}
}