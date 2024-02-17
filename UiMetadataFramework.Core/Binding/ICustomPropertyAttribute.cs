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
		/// Gets name of the custom property.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets value for the custom property.
		/// </summary>
		/// <param name="type">The exact component type. Useful for situations where custom properties
		/// need to be constructed based on the component type itself (e.g. - generic components).</param>
		/// <param name="binder">Metadata binder instance.</param>
		/// <returns>Object representing value of the custom property.</returns>
		object GetValue(Type type, MetadataBinder binder);
	}
}