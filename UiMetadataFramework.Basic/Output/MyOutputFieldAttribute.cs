namespace UiMetadataFramework.Basic.Output;

using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputFieldAttribute : OutputFieldAttribute
{
	/// <summary>
	/// CSS class to apply to the output field.
	/// </summary>
	public string? CssClass { get; set; }
}