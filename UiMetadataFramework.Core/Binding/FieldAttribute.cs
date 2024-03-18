namespace UiMetadataFramework.Core.Binding;

using System;
using System.Reflection;

/// <summary>
/// Retrieves metadata for a field.
/// </summary>
/// <typeparam name="TBinding">Binding type for this field.</typeparam>
/// <typeparam name="TFieldMetadata">Metadata type for this field..</typeparam>
public abstract class FieldAttribute<TBinding, TFieldMetadata> : Attribute
	where TBinding : IComponentBinding
	where TFieldMetadata : IFieldMetadata
{
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
		TBinding binding,
		MetadataBinder binder);
}