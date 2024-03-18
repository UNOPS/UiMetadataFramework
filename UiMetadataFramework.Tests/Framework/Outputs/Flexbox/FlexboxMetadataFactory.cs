namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UiMetadataFramework.Core.Binding;

public class FlexboxMetadataFactory : DefaultMetadataFactory
{
	protected override void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		var value = type.GetProperty(nameof(Flexbox<object>.Value));

		Debug.Assert(value != null, nameof(value) + " != null");

		result["Fields"] = binder.Outputs.GetFields(value!.PropertyType).ToArray();
	}
}