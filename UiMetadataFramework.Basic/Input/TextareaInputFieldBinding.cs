namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	public class TextareaInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "textarea";

		public TextareaInputFieldBinding() : base(typeof(TextareaValue), ControlName)
		{
		}
	}

	public class TextareaValue
	{
		public string Value { get; set; }
	}
}