namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class ObjectListMetadataFactory : DefaultMetadataFactory
{
	protected override void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurationData,
		Dictionary<string, object?> result)
	{
		result["InnerComponent"] = binder.Outputs.BuildComponent(
			type.GenericTypeArguments[0],
			null,
			new ComponentConfigurationAttribute[0]);
	}
}