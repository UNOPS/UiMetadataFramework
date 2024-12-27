namespace UiMetadataFramework.Core.Binding
{
	/// <summary>
	/// Attribute used for decorating input fields.
	/// </summary>
	public class InputFieldAttribute : FieldAttribute
	{
		/// <inheritdoc />
		public InputFieldAttribute() : base(MetadataBinder.ComponentCategories.Input)
		{
		}

		/// <summary>
		/// Gets or sets value indicating whether value for this input field is required
		/// before submitting the form.
		/// </summary>
		public bool Required { get; set; }
	}
}