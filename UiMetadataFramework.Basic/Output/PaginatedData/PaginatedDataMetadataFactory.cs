namespace UiMetadataFramework.Basic.Output.PaginatedData;

using System;
using System.Collections.Generic;
using System.Linq;
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
		ConfigurationDataAttribute[] configurationData,
		Dictionary<string, object?> result)
	{
		try
		{
			var paginatedItemType = type.GenericTypeArguments[0];
			var columns = binder.BuildOutputFields(paginatedItemType).ToList();

			result["Columns"] = columns;
		}
		catch (Exception ex)
		{
			throw new BindingException($"Failed to retrieve custom properties for '{type}'.", ex);
		}
	}
}