namespace UiMetadataFramework.Basic.Inputs.Textarea
{
	/// <summary>
	/// Represents an input field for multiline text.
	/// </summary>
	[MyInputComponent(ControlName)]
	public class TextareaValue
	{
		internal const string ControlName = "textarea";

		/// <summary>
		/// The value of the input field.
		/// </summary>
		public string? Value { get; set; }
	}
}