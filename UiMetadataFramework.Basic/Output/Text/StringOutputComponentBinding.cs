namespace UiMetadataFramework.Basic.Output.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringOutputComponentBinding : ComponentBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringOutputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Output,
			serverTypes:
			[
				typeof(string),
				typeof(bool)
			],
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}