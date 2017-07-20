namespace UiMetadataFramework.Web.Metadata
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public class PaginatedResponse<T> : FormResponse<MyFormResponseMetadata>
	{
		public PaginatedResponse(string title)
		{
			this.Metadata = new MyFormResponseMetadata
			{
				Title = title
			};
		}

		/// <summary>
		/// Gets or sets items.
		/// </summary>
		[OutputField(OrderIndex = 100)]
		public IEnumerable<T> Results { get; set; }

		/// <summary>
		/// Gets or sets total number of matching items in the data store.
		/// </summary>
		[OutputField(Hidden = true)]
		public int TotalCount { get; set; }
	}
}