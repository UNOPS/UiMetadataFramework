namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	public class BooleanInputFieldBinding : InputFieldBinding
	{
		public const string ControlName = "boolean";

		public BooleanInputFieldBinding() : base(typeof(bool), ControlName)
		{
		}
	}
}