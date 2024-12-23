namespace UiMetadataFramework.Core.Binding;

using System;
using System.Reflection;

/// <summary>
/// Retrieves metadata for a field.
/// </summary>
public abstract class FieldAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="FieldAttribute"/> class.
	/// </summary>
	protected FieldAttribute(string category)
	{
		this.Category = category;
	}

	/// <summary>
	/// Component category that this field attribute supports.
	/// </summary>
	public string Category { get; }

	/// <summary>
	/// Gets or sets value indicating whether this field should be visible or not.
	/// </summary>
	public bool Hidden { get; set; }

	/// <summary>
	/// Gets or sets label for the field.
	/// </summary>
	public string? Label { get; set; }

	/// <summary>
	/// Gets or sets value which will dictate rendering position of this field
	/// in relationship to other input fields.
	/// </summary>
	public int OrderIndex { get; set; }

	/// <summary>
	/// Gets metadata for the output field decorated with this attribute.
	/// </summary>
	/// <param name="property">Output field that has been decorated with this attribute.</param>
	/// <param name="binding">Binding for the output field.</param>
	/// <param name="binder">Metadata binder.</param>
	/// <returns>Instance of <see cref="OutputFieldMetadata"/>.</returns>
	/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
	public abstract FieldMetadata GetMetadata(
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder);
}