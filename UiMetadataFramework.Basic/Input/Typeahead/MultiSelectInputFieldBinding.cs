﻿namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Binding;

	[InputFieldType("multiselect")]
	public class MultiSelect<T>
	{
		public MultiSelect()
		{
		}

		public MultiSelect(params T[] values)
		{
			this.Items = this.Items ?? new List<T>();
			foreach (var value in values.Distinct())
			{
				this.Items.Add(value);
			}
		}

		public IList<T>? Items { get; set; }
	}
}