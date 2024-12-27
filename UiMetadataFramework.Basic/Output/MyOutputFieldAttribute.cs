namespace UiMetadataFramework.Basic.Output;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputFieldAttribute : OutputFieldAttribute
{
	/// <summary>
	/// CSS class to apply to the output field.
	/// </summary>
	public string? CssClass { get; set; }

	/// <inheritdoc />
	public override FieldMetadata GetMetadata(
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		var noLabelByDefault = binding.GetAdditionalData<bool?>(nameof(MyOutputComponentAttribute.NoLabelByDefault));

		if (noLabelByDefault != null)
		{
			basic.Label = this.Label ?? (noLabelByDefault.Value ? "" : null) ?? property.Name;
		}

		var defaultOrderIndex = binding.GetAdditionalData<int?>(nameof(MyOutputComponentAttribute.DefaultOrderIndex));

		if (defaultOrderIndex != null)
		{
			basic.OrderIndex = this.OrderIndex == 0
				? defaultOrderIndex.Value
				: this.OrderIndex;
		}

		return new Metadata(basic) { CssClass = this.CssClass };
	}

	/// <inheritdoc />
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