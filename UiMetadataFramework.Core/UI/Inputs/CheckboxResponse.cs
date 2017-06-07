namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Models;

	public class CheckboxResponse : CheckboxResponse<CheckboxItem, bool>
	{
	}

	public class CheckboxResponse<TItem, TItemValue>
		where TItem : CheckboxItem<TItemValue>
	{
		public List<TItem> Items { get; set; }
	}

	public class CheckboxResponse<TItemValue>
	{
		public List<CheckboxItem<TItemValue>> Items { get; set; }
	}
}