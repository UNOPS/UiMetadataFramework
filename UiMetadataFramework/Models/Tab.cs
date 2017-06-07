namespace UiMetadataFramework.Core.Models
{
	public class Tab
	{
		public Tab(string id, string label)
		{
			this.Id = id;
			this.Label = label;
		}

		public Tab()
		{
		}

		public string Id { get; set; }
		public string Label { get; set; }
	}
}