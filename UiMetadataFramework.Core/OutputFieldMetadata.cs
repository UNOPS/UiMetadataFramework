namespace UiMetadataFramework.Core
{
	public class OutputFieldMetadata : IFieldMetadata
	{
		public string Id { get; set; }
		public string Label { get; set; }
		public string Type { get; set; }
		public bool Hidden { get; set; }
		public int OrderIndex { get; set; }
	}
}