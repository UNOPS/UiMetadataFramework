namespace UiMetadataFramework.Basic.Inputs.Typeahead
{
	/// <summary>
	/// Represents an option inside a typeahead input.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class TypeaheadItem<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadItem{T}"/> class.
		/// </summary>
		public TypeaheadItem()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadItem{T}"/> class.
		/// </summary>
		public TypeaheadItem(string label, T value)
		{
			this.Label = label;
			this.Value = value;
		}

		/// <summary>
		/// Descriptive label for the item.
		/// </summary>
		public string? Label { get; set; }

		/// <summary>
		/// Unique identifier for the item.
		/// </summary>
		public T? Value { get; set; }
	}
}