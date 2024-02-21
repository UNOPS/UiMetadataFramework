namespace UiMetadataFramework.Basic.Inputs.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringInputComponentBinding : InputComponentBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringInputComponentBinding() : base(
			serverType: typeof(string),
			clientType: ControlName,
			metadataFactory: null)
		{
		}
	}
}