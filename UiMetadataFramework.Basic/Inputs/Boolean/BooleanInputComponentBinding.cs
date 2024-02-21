namespace UiMetadataFramework.Basic.Inputs.Boolean
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class BooleanInputComponentBinding : InputComponentBinding
	{
		internal const string ControlName = "boolean";

		/// <inheritdoc />
		public BooleanInputComponentBinding() : base(
			serverType: typeof(bool),
			clientType: ControlName,
			metadataFactory: null)
		{
		}
	}
}