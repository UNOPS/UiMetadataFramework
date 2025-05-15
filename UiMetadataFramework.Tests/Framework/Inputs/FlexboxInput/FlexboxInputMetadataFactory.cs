namespace UiMetadataFramework.Tests.Framework.Inputs.FlexboxInput;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UiMetadataFramework.Core.Binding;

public class FlexboxInputMetadataFactory : DefaultMetadataFactory
{
	protected override void AugmentConfiguration(
		Type type,
		Type? derivedType,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurationData,
		Dictionary<string, object?> result)
	{
		Debug.Assert(derivedType != null);

		result["Fields"] = binder.Inputs.GetFields(derivedType!, true).ToList();
	}
}