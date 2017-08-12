namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System.Collections.Generic;

	public interface ITypeaheadInlineSource<T>
	{
		IEnumerable<TypeaheadItem<T>> GetItems();
	}
}