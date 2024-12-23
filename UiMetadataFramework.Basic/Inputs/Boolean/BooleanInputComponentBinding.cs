namespace UiMetadataFramework.Basic.Inputs.Boolean
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class BooleanInputComponentBinding : ComponentBinding
	{
		/// <inheritdoc />
		public BooleanInputComponentBinding() : base(
			MetadataBinder.ComponentCategories.Input,
			serverType: typeof(bool),
			componentType: "boolean",
			metadataFactory: null)
		{
		}
	}
}