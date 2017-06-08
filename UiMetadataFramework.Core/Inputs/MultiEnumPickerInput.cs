namespace UiMetadataFramework.Core.Inputs
{
	using System;
	using System.Linq;
	using UiMetadataFramework.Core.Models;

	public class MultiEnumPickerInput<T> : MultiSelectInput
	{
		public MultiEnumPickerInput(string name) : base(name, MultiselectTypename)
		{
			this.Items = GetItems();
		}

		private static MultiSelectItem[] GetItems()
		{
			return Enum.GetValues(typeof(T)).Cast<Enum>().Select(t => new MultiSelectItem(t.ToString(), t.ToString())).ToArray();
		}
	}
}