namespace UiMetadataFramework.Basic.Output.PaginatedData;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Metadata factory for <see cref="PaginatedData"/>.
/// </summary>
public class PaginatedDataMetadataFactory : DefaultMetadataFactory
{
	/// <inheritdoc />
	protected override void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		result["Columns"] = binder.Outputs.GetFields(type.GenericTypeArguments[0]).ToList();
	}
}