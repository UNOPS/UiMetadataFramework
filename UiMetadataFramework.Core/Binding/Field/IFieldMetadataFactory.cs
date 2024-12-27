namespace UiMetadataFramework.Core.Binding;

using System.Reflection;

/// <summary>
/// Builds field metadata.
/// </summary>
public interface IFieldMetadataFactory
{
	/// <summary>
	/// Gets metadata for the output field decorated with this attribute.
	/// </summary>
	/// <param name="attribute">Field attribute.</param>
	/// <param name="property">Output field that has been decorated with this attribute.</param>
	/// <param name="binding">Binding for the output field.</param>
	/// <param name="binder">Metadata binder.</param>
	/// <returns>Instance of <see cref="FieldMetadata"/>.</returns>
	/// <remarks>This method will be used internally by <see cref="MetadataBinder"/>.</remarks>
	FieldMetadata GetMetadata(
		FieldAttribute? attribute,
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder);
}