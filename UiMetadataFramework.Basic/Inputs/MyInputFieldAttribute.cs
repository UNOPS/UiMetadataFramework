namespace UiMetadataFramework.Basic.Inputs;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyInputFieldAttribute : InputFieldAttribute
{
	/// <inheritdoc />
	public override InputFieldMetadata GetMetadata(
		PropertyInfo property,
		InputComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		if (binding.AdditionalData?.TryGetValue(nameof(MyInputComponentAttribute.AlwaysHidden), out var alwaysHidden) == true)
		{
			if (alwaysHidden is bool hide)
			{
				basic.Hidden = hide || basic.Hidden;
			}
		}

		return basic;
	}
}