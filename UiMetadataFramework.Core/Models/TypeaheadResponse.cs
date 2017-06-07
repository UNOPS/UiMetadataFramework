namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;

	public class TypeaheadResponse : TypeaheadResponse<TypeaheadItem, long>
	{
	}

	public class TypeaheadResponse<TItemValue> : TypeaheadResponse<TypeaheadItem<TItemValue>, TItemValue>
	{
	}

	public class TypeaheadResponse<TItem, TItemValue>
		where TItem : TypeaheadItem<TItemValue>
	{
		public List<TItem> Items { get; set; }

		/// <summary>
		/// Total number of items inside source/>
		/// (without any query, i.e. - <see cref="TypeaheadRequest"/>). If null, then it means
		/// source has unspecified number of items.
		/// </summary>
		public int? TotalItemCount { get; set; }
	}
}