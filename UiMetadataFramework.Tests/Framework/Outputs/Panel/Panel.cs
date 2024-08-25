namespace UiMetadataFramework.Tests.Framework.Outputs.Panel;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

[OutputComponent("panel", typeof(PanelMetadataFactory))]
public abstract class Panel;

public class PanelMetadataFactory : DefaultMetadataFactory
{
	public const string Properties = "Properties";

	public static IList<OutputFieldMetadata> ParseConfiguration(object configuration)
	{
		var dic = (Dictionary<string, object?>)configuration;
		return (IList<OutputFieldMetadata>)dic[Properties]!;
	}

	protected override void AugmentConfiguration(
		Type type,
		Type? derivedType,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		if (derivedType == null)
		{
			throw new Exception("Panel must be subclassed.");
		}

		result[Properties] = binder.Outputs.GetFields(derivedType).ToList();
	}
}