namespace UiMetadataFramework.Basic.Inputs.Typeahead
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a data source which can retrieve items for a typeahead.
	/// Classes implementing this interface are assumed to be "static",
	/// meaning that the list of items they retrieve never changes.
	/// </summary>
	public interface ITypeaheadInlineSource<T>
	{
		/// <summary>
		/// Retrieves the list of items for the typeahead.
		/// </summary>
		IEnumerable<TypeaheadItem<T>> GetItems();
	}
}