namespace UiMetadataFramework.Basic.Inputs.Paginator
{
	using UiMetadataFramework.Core.Binding;

	/// <inheritdoc />
	public class PaginatorInputFieldBinding : InputFieldBinding
	{
		/// <inheritdoc />
		public PaginatorInputFieldBinding() : base(typeof(Paginator), "paginator")
		{
			this.IsInputAlwaysHidden = true;
		}
	}
}