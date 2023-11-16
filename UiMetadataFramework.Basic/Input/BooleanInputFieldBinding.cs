namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class BooleanInputFieldBinding : InputFieldBinding
	{
		internal const string ControlName = "boolean";

		/// <inheritdoc />
		public BooleanInputFieldBinding() : base(typeof(bool), ControlName)
		{
		}
	}
}