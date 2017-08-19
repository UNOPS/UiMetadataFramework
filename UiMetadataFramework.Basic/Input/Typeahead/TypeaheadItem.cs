namespace UiMetadataFramework.Basic.Input.Typeahead
{
	public class TypeaheadItem<T>
	{
		public TypeaheadItem()
		{
		}

		public TypeaheadItem(string label, T value)
		{
			this.Label = label;
			this.Value = value;
		}

		public string Label { get; set; }
		public T Value { get; set; }
	}
}