namespace UiMetadataFramework.Basic.Output.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringOutputComponentBinding : OutputComponentBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringOutputComponentBinding() : base(
			serverTypes: new[]
			{
				typeof(string),
				typeof(bool)
			},
			clientType: ControlName,
			metadataFactory: null)
		{
		}
	}
}