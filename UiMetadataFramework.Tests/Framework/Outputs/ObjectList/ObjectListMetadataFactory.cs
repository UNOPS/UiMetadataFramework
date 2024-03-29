namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

public class ObjectListMetadataFactory : DefaultMetadataFactory
{
	protected override void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		result["InnerComponent"] = binder.Outputs.BuildComponent(type.GenericTypeArguments[0]);
	}
}