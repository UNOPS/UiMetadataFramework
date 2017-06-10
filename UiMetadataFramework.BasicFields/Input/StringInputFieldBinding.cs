namespace UiMetadataFramework.BasicFields.Input
{
	using UiMetadataFramework.Core.Binding;

	public class StringInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "text";

		public StringInputFieldBinding() : base(typeof(string), ControlName)
		{
		}
	}
}