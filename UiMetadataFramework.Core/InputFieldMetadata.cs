namespace UiMetadataFramework.Core
{
	/// <summary>
	/// <see cref="FieldMetadata"/> for an input component.
	/// </summary>
	public class InputFieldMetadata : FieldMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InputFieldMetadata"/> class.
		/// </summary>
		public InputFieldMetadata(Component component) : base(component)
		{
		}

		/// <summary>
		/// Creates a deep copy of the specified <see cref="InputFieldMetadata"/>.
		/// </summary>
		/// <param name="metadata">Instance to copy.</param>
		protected InputFieldMetadata(InputFieldMetadata metadata) : base(metadata)
		{
			this.Required = metadata.Required;
		}

		/// <summary>
		/// Indicates if a value for this input field is required.
		/// </summary>
		public bool Required { get; set; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ToDescriptiveString();
		}
	}
}