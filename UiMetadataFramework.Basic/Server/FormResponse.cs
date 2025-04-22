namespace UiMetadataFramework.Basic.Server;

using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Represents response of a form.
/// </summary>
/// <typeparam name="T">Type representing form metadata.</typeparam>
public class FormResponse<T> where T : FormResponseMetadata
{
	/// <summary>
	/// Represents response which has additional metadata describing how to render the results.
	/// </summary>
	[NotField]
	public T? Metadata { get; set; }
}

/// <summary>
/// Default form response type without any custom metadata.
/// </summary>
public class FormResponse : FormResponse<FormResponseMetadata>
{
	/// <summary>
	/// Creates a new instance of <see cref="FormResponse"/> class with default
	/// handler (i.e. "null"). 
	/// </summary>
	public FormResponse()
	{
	}

	/// <summary>
	/// Creates a new instance of <see cref="FormResponse"/> class, with specified 
	/// <see cref="FormResponse"/> handler.
	/// </summary>
	/// <param name="handler"></param>
	public FormResponse(string handler)
	{
		this.Metadata = new FormResponseMetadata(handler);
	}
}