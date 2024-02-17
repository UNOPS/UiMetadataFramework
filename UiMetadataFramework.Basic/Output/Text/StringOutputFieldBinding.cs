namespace UiMetadataFramework.Basic.Output.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringOutputFieldBinding : OutputFieldBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringOutputFieldBinding() : base(
			serverTypes: new[]
			{
				typeof(string),
				typeof(bool)
			},
			clientType: ControlName,
			mandatoryCustomProperty: null,
			metadataFactory: null)
		{
		}
	}
}