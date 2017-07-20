namespace UiMetadataFramework.Web.Metadata
{
	using UiMetadataFramework.Core.Binding;

	public class PaginatedRequest
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