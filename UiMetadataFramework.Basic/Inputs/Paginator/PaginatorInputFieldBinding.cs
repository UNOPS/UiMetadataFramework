namespace UiMetadataFramework.Basic.Inputs.Paginator
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class PaginatorInputFieldBinding : InputFieldBinding
	{
		/// <inheritdoc />
		public PaginatorInputFieldBinding() : base(
			serverType: typeof(Paginator),
			clientType: "paginator",
			metadataFactory: null)
		{
			this.IsInputAlwaysHidden = true;
		}
	}
}