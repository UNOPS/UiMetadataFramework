namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	public class PaginatedRequest
	{
		[InputField(Hidden = true)]
		public bool Ascending { get; set; }

		[InputField(Hidden = true)]
		public string OrderBy { get; set; }

		[InputField(Hidden = true)]
		public int PageIndex { get; set; }

		[InputField(Hidden = true)]
		public int PageSize { get; set; }
	}

	public class PaginatedRequestFieldBinding : InputFieldBinding
	{
		public PaginatedRequestFieldBinding() : base(typeof(PaginatedRequest), "paginator")
		{
			this.IsInputAlwaysHidden = true;
		}
	}
}