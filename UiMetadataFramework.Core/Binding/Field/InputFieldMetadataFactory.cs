namespace UiMetadataFramework.Core.Binding;

using System;
using System.Reflection;

/// <inheritdoc />
public class InputFieldMetadataFactory : DefaultFieldMetadataFactory
{
	/// <inheritdoc />
	public override FieldMetadata GetMetadata(
		FieldAttribute? attribute,
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(
			attribute,
			property,
			binding,
			binder);

		var propertyType = property.PropertyType.IsConstructedGenericType && !property.PropertyType.IsNullabble()
			? property.PropertyType.GetGenericTypeDefinition()
			: property.PropertyType;

		var required = propertyType.GetTypeInfo().IsValueType
			// non-nullable value types are automatically required,
			// nullable types are automatically NOT required.
			? Nullable.GetUnderlyingType(propertyType) == null
			// reference types use attribute
			: (attribute as InputFieldAttribute)?.Required ?? false;

		return new Metadata(basic) { Required = required };
	}

	/// <summary>
	/// Metadata for input fields.
	/// </summary>
	/// <param name="metadata"></param>
	public class Metadata(FieldMetadata metadata) : FieldMetadata(metadata)
	{
		/// <summary>
		/// Indicates if a value for this input field is required.
		/// </summary>
		public bool Required { get; set; }
	}
}