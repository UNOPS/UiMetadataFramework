namespace UiMetadataFramework.Basic.Inputs;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyInputFieldAttribute : InputFieldAttribute
{
	/// <inheritdoc />
	public override FieldMetadata GetMetadata(
		PropertyInfo property,
		ComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		if (!basic.Hidden)
		{
			if (binding.AdditionalData?.TryGetValue(nameof(MyInputComponentAttribute.AlwaysHidden), out var alwaysHidden) == true)
			{
				if (alwaysHidden is bool hide)
				{
					basic.Hidden = hide || basic.Hidden;
				}
			}	
		}

		if (string.IsNullOrWhiteSpace(this.Label))
		{
			var defaultLabel = binding.AdditionalData?.TryGetValue(nameof(MyInputComponentAttribute.DefaultLabel), out var label) == true
				? label?.ToString()
				: null;
			
			basic.Label = defaultLabel ?? property.Name;
		}

		return basic;
	}
}