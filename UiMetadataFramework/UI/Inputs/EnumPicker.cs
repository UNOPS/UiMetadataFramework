namespace UiMetadataFramework.Core.UI.Inputs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Models;

	public class EnumPicker<T> : DropdownList
	{
		public EnumPicker(string name, params Enum[] values) : base(name, GetItems(values))
		{
		}

		public EnumPicker(string name) : base(name, GetItems())
		{
		}

		private static DropdownItem[] GetItems(IEnumerable<Enum> values)
		{
			return values.Select(t => new DropdownItem(t.ToString(), t.ToString())).ToArray();
		}

		private static DropdownItem[] GetItems()
		{
			return Enum.GetValues(typeof(T)).Cast<Enum>().Select(t => new DropdownItem(t.ToString(), t.ToString())).ToArray();
		}
	}
}