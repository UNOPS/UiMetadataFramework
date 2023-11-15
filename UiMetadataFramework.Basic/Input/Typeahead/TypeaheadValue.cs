namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Input field type for typeahead client control.
	/// </summary>
	/// <typeparam name="T">Type of values retrieved by the typeahead.</typeparam>
	[InputFieldType("typeahead")]
	public class TypeaheadValue<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadValue{T}"/> class.
		/// </summary>
		public TypeaheadValue()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadValue{T}"/> class.
		/// </summary>
		public TypeaheadValue(T value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets value for the typeahead. The value represents a single item
		/// selected in the typeahead client control.
		/// </summary>
		public T? Value { get; set; }
	}
}