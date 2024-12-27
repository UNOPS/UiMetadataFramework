namespace UiMetadataFramework.Basic.Inputs;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyInputFieldMetadataFactory : InputFieldMetadataFactory
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

		if (!basic.Hidden)
		{
			var alwaysHidden = binding.GetAdditionalData<bool?>(nameof(MyInputComponentAttribute.AlwaysHidden));

			if (alwaysHidden != null)
			{
				basic.Hidden = alwaysHidden.Value || basic.Hidden;
			}
		}

		if (string.IsNullOrWhiteSpace(attribute?.Label))
		{
			var defaultLabel = binding.GetAdditionalData<string?>(nameof(MyInputComponentAttribute.DefaultLabel));

			basic.Label = defaultLabel ?? property.Name;
		}

		return basic;
	}
}