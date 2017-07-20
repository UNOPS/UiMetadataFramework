namespace UiMetadataFramework.Web.Metadata
{
	using UiMetadataFramework.Core;

	public class MyFormResponseMetadata : FormResponseMetadata
	{
		public MyFormResponseMetadata()
		{
		}

		public MyFormResponseMetadata(string handler) : base(handler)
		{
		}

		/// <summary>
		/// Gets or sets heading to show for this particular response instance.
		/// </summary>
		public string Title { get; set; }
	}
}