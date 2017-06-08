namespace UiMetadataFramework.Core
{
	public class InputFieldMetadata : IFieldMetadata
	{
		public InputFieldSource DefaultValue { get; set; }
		public string Id { get; set; }
		public string Label { get; set; }
		public string Type { get; set; }
		public bool Hidden { get; set; }
		public int OrderIndex { get; set; }
		public bool Required { get; set; }
	}
}