namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents a client-side function.
	/// </summary>
	public interface IClientFunctionMetadata
	{
		/// <summary>
		/// Gets or sets custom properties describing how to run the function.
		/// </summary>
		object CustomProperties { get; }

		/// <summary>Gets or sets id of the function.</summary>
		string Id { get; }
	}
}