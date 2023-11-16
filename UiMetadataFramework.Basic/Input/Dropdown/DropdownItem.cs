namespace UiMetadataFramework.Basic.Input.Dropdown
{
	/// <summary>
	/// Represents an item in a dropdown.
	/// </summary>
	public class DropdownItem
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DropdownItem"/> class.
		/// </summary>
		public DropdownItem(string label, string value)
		{
			this.Value = value;
			this.Label = label;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DropdownItem"/> class.
		/// </summary>
		public DropdownItem()
		{
		}

		/// <summary>
		/// Descriptive label for the item.
		/// </summary>
		public string? Label { get; set; }

		/// <summary>
		/// Unique identifier for the item.
		/// </summary>
		public string? Value { get; set; }
	}
}