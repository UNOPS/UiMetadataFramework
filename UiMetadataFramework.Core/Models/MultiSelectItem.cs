namespace UiMetadataFramework.Core.Models
{
	public class MultiSelectItem
	{
		public MultiSelectItem()
		{
		}

		public MultiSelectItem(string label, string value)
		{
			this.Label = label;
			this.Value = value;
		}

		public string Label { get; set; }
		public string Value { get; set; }
	}
}