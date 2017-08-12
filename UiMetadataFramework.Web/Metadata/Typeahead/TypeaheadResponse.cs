namespace UiMetadataFramework.Web.Metadata.Typeahead
{
	using System.Collections.Generic;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Core;

	public class TypeaheadResponse<T> : FormResponse
	{
		public IEnumerable<TypeaheadItem<T>> Items { get; set; }
		public int TotalItemCount { get; set; }
	}
}