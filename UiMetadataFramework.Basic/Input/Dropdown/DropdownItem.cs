namespace UiMetadataFramework.Basic.Input.Dropdown
{
	public class DropdownItem
	{
		public DropdownItem(string label, string value)
		{
			this.Value = value;
			this.Label = label;
		}

		public DropdownItem()
		{
		}

		public string Label { get; set; }
		public string Value { get; set; }
	}
}