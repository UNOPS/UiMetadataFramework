namespace UiMetadataFramework.MediatR
{
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents response of a form.
	/// </summary>
	public class FormResponse<T> : IFormResponse<T>
		where T : FormResponseMetadata
	{
		/// <summary>
		/// Represents response which has additional metadata describing how to render the results.
		/// </summary>
		[NotField]
		public T? Metadata { get; set; }
	}
}