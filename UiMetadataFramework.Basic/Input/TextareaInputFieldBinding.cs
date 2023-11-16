namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents an input field for multiline text.
	/// </summary>
	[InputFieldType(ControlName)]
	public class TextareaValue
	{
		internal const string ControlName = "textarea";

		/// <summary>
		/// The value of the input field.
		/// </summary>
		public string? Value { get; set; }
	}
}