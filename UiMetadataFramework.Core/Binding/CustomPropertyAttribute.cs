namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Can be applied to an input or output field to add additional metadata.
	/// The metadata will go into the <see cref="IFieldMetadata.CustomProperties"/>
	/// as a new property.
	/// </summary>
	public class CustomPropertyAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of <see cref="CustomPropertyAttribute"/> class.
		/// </summary>
		/// <param name="name">Name of the custom property.</param>
		/// <param name="value">Value of the custom property.</param>
		public CustomPropertyAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets name of the custom property.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets value for the custom property.
		/// </summary>
		public string Value { get; set; }
	}
}