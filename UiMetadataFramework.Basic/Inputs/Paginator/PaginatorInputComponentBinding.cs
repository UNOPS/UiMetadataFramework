namespace UiMetadataFramework.Basic.Inputs.Paginator
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class PaginatorInputComponentBinding : InputComponentBinding
	{
		/// <inheritdoc />
		public PaginatorInputComponentBinding() : base(
			serverType: typeof(Paginator),
			clientType: "paginator",
			metadataFactory: null)
		{
			this.IsInputAlwaysHidden = true;
		}
	}
}