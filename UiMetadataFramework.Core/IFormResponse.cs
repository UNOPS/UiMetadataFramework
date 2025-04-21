namespace UiMetadataFramework.Core;

using UiMetadataFramework.Core.Binding;

/// <summary>
/// Represents response of a form.
/// </summary>
/// <typeparam name="T">Type representing form metadata.</typeparam>
public interface IFormResponse<out T> where T : FormResponseMetadata
{
	/// <summary>
	/// Represents response which has additional metadata describing how to render the results.
	/// </summary>
	[NotField]
	public T? Metadata { get; }
}