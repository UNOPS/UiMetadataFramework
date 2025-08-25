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
		Type? derivedType,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		Debug.Assert(derivedType != null);

		result["Fields"] = binder.Outputs.GetFields(derivedType).ToArray();
	}
}