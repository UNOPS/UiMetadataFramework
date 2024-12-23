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

		if (binding.AdditionalData?.TryGetValue(nameof(MyOutputComponentAttribute.NoLabelByDefault), out var noLabelByDefault) == true)
		{
			if (noLabelByDefault is bool noLabel)
			{
				basic.Label = this.Label ?? (noLabel ? "" : null) ?? property.Name;
			}
		}

		if (binding.AdditionalData?.TryGetValue(nameof(MyOutputComponentAttribute.DefaultOrderIndex), out var defaultOrderIndex) == true)
		{
			if (defaultOrderIndex is int orderIndex)
			{
				basic.OrderIndex = this.OrderIndex == 0
					? orderIndex
					: this.OrderIndex;
			}
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