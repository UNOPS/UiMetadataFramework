namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Binding;

	public class MultiSelectInputFieldBinding : TypeaheadInputFieldBindingBase
	{
		public MultiSelectInputFieldBinding(DependencyInjectionContainer container) : base(typeof(MultiSelect<>), "multiselect", container)
		{
		}
	}

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

		public IList<T> Items { get; set; }
	}
}