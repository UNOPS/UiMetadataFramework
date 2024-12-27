namespace UiMetadataFramework.Basic.Output;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputFieldMetadataFactory : DefaultFieldMetadataFactory
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

		var noLabelByDefault = binding.GetAdditionalData<bool?>(nameof(MyOutputComponentAttribute.NoLabelByDefault));

		if (noLabelByDefault != null)
		{
			basic.Label = attribute?.Label ?? (noLabelByDefault.Value ? "" : null) ?? property.Name;
		}

		var defaultOrderIndex = binding.GetAdditionalData<int?>(nameof(MyOutputComponentAttribute.DefaultOrderIndex));

		if (defaultOrderIndex != null)
		{
			basic.OrderIndex = attribute?.OrderIndex == 0
				? defaultOrderIndex.Value
				: attribute?.OrderIndex ?? 0;
		}

		return new Metadata(basic) { CssClass = (attribute as MyOutputFieldAttribute)?.CssClass };
	}

	public class Metadata : FieldMetadata
	{
		/// <inheritdoc />
		public Metadata(FieldMetadata metadata) : base(metadata)
		{
		}

		/// <summary>
		/// CSS class to apply to the output field.
		/// </summary>
		public string? CssClass { get; set; }
	}
}