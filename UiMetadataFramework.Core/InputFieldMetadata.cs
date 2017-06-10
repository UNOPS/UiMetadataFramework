namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents metadata for a single input field.
	/// </summary>
	public class InputFieldMetadata : IFieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldMetadata"/> class.
		/// </summary>
		/// <param name="type">Name of the client control which will be responsible for rendering
		/// this output field.</param>
		public InputFieldMetadata(string type)
		{
			this.Type = type;
		}

		/// <summary>
		/// Gets or sets additional parameters for the client control.
		/// </summary>
		public object CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets source from which the default value for the input field will be taken.
		/// If null, then the field will not have a default value.
		/// </summary>
		public InputFieldSource DefaultValue { get; set; }

		/// <summary>
		/// Gets or sets value indicating wheather value for this input field is required
		/// before submitting the form.
		/// </summary>
		public bool Required { get; set; }

		/// <summary>
		/// Gets or sets id of the field to which this metadata belongs.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets label for the output field.
		/// </summary>
		public string Label { get; set; }

		/// <summary>
		/// Gets name of the client control which will render this output field.
		/// </summary>
		public string Type { get; protected set; }

		/// <summary>
		/// Gets or sets value indicating whether this field should be visible or not.
		/// </summary>
		public bool Hidden { get; set; }

		/// <summary>
		/// Gets or sets value which will dictate rendering position of this field
		/// in relationship to output fields within the same <see cref="FormResponse"/>.
		/// </summary>
		public int OrderIndex { get; set; }
	}
}