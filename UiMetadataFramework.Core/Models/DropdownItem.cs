namespace UiMetadataFramework.Core.Models
{
	public class DropdownItem
	{
		public DropdownItem()
		{
		}

		public DropdownItem(string label, string value)
		{
			this.Label = label;
			this.Value = value;
		}

		public string Label { get; set; }
		public string Value { get; set; }
	}
}