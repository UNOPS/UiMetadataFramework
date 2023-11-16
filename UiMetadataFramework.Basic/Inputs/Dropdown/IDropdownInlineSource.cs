namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a data source which can retrieve items for a dropdown.
	/// Classes implementing this interface are assumed to be "static"
	/// data sources, meaning that the list of values never changes. 
	/// </summary>
	public interface IDropdownInlineSource
	{
		/// <summary>
		/// Gets the list of items for the dropdown.
		/// </summary>
		IEnumerable<DropdownItem> GetItems();
	}
}