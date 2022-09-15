namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	[InputFieldType(ControlName)]
	public class TextareaValue
	{
		public const string ControlName = "textarea";
		
		public string Value { get; set; }
	}
}