namespace UiMetadataFramework.Tests.Framework.Inputs;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class MyInputFieldAttribute : InputFieldAttribute
{
	public override InputFieldMetadata GetMetadata(
		PropertyInfo property,
		InputComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		if (binding.AdditionalData?.GetValueOrDefault(nameof(MyInputComponentAttribute.DefaultLabel)) is string defaultLabel)
		{
			basic.Label = this.Label == null
				? defaultLabel
				: basic.Label;
		}

		return basic;
	}
}