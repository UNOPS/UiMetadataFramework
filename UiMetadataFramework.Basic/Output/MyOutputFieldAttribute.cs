namespace UiMetadataFramework.Basic.Output;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputFieldAttribute : OutputFieldAttribute
{
	/// <inheritdoc />
	public override OutputFieldMetadata GetMetadata(
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

		return basic;
	}
}