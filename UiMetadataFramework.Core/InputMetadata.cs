namespace UiMetadataFramework.Core
{
	public class InputMetadata
	{
		public InputMetadata(string name, string type, object parameters = null)
		{
			this.Name = name;
			this.Type = type;
			this.Parameters = parameters;
		}

		public object DefaultValue { get; set; }

		public string Label { get; set; }
		public string Name { get; }
		public int OrderIndex { get; set; } = int.MaxValue;
		public object Parameters { get; set; }
		public bool Required { get; set; }
		public string Type { get; internal set; }
	}
}