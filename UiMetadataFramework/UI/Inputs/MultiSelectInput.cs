namespace UiMetadataFramework.Core.UI.Inputs
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Models;

	public class MultiSelectInput : Input
	{
		public const string MultiselectTypename = "multiselect";

		public MultiSelectInput(string name, string source) : base(name, MultiselectTypename, source)
		{
		}

		public MultiSelectInput(string name, params Enum[] values) : base(name, MultiselectTypename)
		{
			this.Items = GetItems(values);
		}

		public MultiSelectInput(string name, params MultiSelectItem[] items) : base(name, MultiselectTypename)
		{
			this.Items = items;
		}

		public MultiSelectInput(string name, IEnumerable<string> items) : base(name, MultiselectTypename)
		{
			this.Items = items.Select(t => new MultiSelectItem(t, t)).ToArray();
		}

		public bool AllowNewValues { get; set; }

		public MultiSelectItem[] Items { get; set; }

		private static MultiSelectItem[] GetItems(IEnumerable<Enum> values)
		{
			return values.Select(t => new MultiSelectItem(t.ToString(), t.ToString())).ToArray();
		}
	}
}