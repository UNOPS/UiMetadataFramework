namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	public class PaginatorInputFieldBinding : InputFieldBinding
	{
		public PaginatorInputFieldBinding() : base(typeof(Paginator), "paginator")
		{
			this.IsInputAlwaysHidden = true;
		}
	}

	public class Paginator
	{
		[InputField(Hidden = true)]
		public bool? Ascending { get; set; }

		[InputField(Hidden = true, Required = false)]
		public string OrderBy { get; set; }

		[InputField(Hidden = true)]
		public int? PageIndex { get; set; }

		[InputField(Hidden = true)]
		public int? PageSize { get; set; }
	}
}