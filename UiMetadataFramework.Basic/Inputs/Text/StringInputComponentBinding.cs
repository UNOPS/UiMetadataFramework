namespace UiMetadataFramework.Basic.Inputs.Text
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class StringInputComponentBinding : ComponentBinding
	{
		internal const string ControlName = "text";

		/// <inheritdoc />
		public StringInputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Input,
			serverType: typeof(string),
			componentType: ControlName,
			metadataFactory: null)
		{
		}
	}
}