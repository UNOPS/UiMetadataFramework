namespace UiMetadataFramework.Tests.Framework.Outputs.ComplexOutput;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

[MyOutputComponent(Type, typeof(ComplexOutputMetadataFactory))]
public abstract class ComplexOutput
{
	public const string Type = "complex-output";
}

public class ComplexOutputMetadataFactory : DefaultMetadataFactory
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
			throw new Exception("Complex output must be subclassed.");
		}

		result[Properties] = binder.Outputs.GetFields(derivedType).ToList();
	}
}