namespace UiMetadataFramework.Core.Binding;

using System;
using System.Reflection;

/// <summary>
/// Retrieves metadata for a field.
/// </summary>
/// <typeparam name="TFieldMetadata">Metadata type for this field..</typeparam>
public abstract class FieldAttribute<TFieldMetadata> : Attribute
	where TFieldMetadata : IFieldMetadata
{
	/// <summary>
	/// Initializes a new instance of the <see cref="FieldAttribute{TFieldMetadata}"/> class.
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
	/// Gets metadata for the output field decorated with this attribute.
	/// </summary>
	/// <param name="property">Output field that has been decorated with this attribute.</param>
	/// <param name="binding">Binding for the output field.</param>
	/// <param name="binder">Metadata binder.</param>
	/// <returns>Instance of <see cref="OutputFieldMetadata"/>.</returns>
	/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
	public abstract TFieldMetadata GetMetadata(
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder);
}