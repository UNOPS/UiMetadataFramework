namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Represents an <see cref="Attribute"/> which can be applied to an input field, output field 
	/// or to a form. The attribute is used to add additional metadata to
	/// <see cref="IFieldMetadata.CustomProperties"/> or <see cref="FormMetadata.CustomProperties"/>.
	/// </summary>
	public interface ICustomPropertyAttribute
	{
		/// <summary>
		/// Gets or sets name of the custom property.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets value for the custom property.
		/// </summary>
		/// <returns>Object representing value of the custom property.</returns>
		object GetValue();
	}
}