namespace UiMetadataFramework.Basic.Inputs.Boolean
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class BooleanInputComponentBinding : InputComponentBinding
	{
		/// <inheritdoc />
		public BooleanInputComponentBinding() : base(
			serverType: typeof(bool),
			clientType: "boolean",
			metadataFactory: null)
		{
		}
	}
}