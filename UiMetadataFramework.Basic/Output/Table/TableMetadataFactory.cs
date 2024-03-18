namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Builds metadata for a generic component such as <see cref="IEnumerable{T}"/>
/// which can later be used to render a table.  
/// </summary>
public class TableMetadataFactory : DefaultMetadataFactory
{
	/// <inheritdoc />
	protected override void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurations,
		Dictionary<string, object?> result)
	{
		var innerType = type.IsArray
			? type.GetElementType() ??
			throw new BindingException($"Cannot get element type from array '{type.FullName}'.")
			: type.GenericTypeArguments[0];

		var isKnownOutputType = binder.Outputs.Bindings.All.Any(t => t.Key.ImplementsClass(innerType));

		result["Columns"] = isKnownOutputType
			? new OutputFieldMetadata(binder.Outputs.BuildComponent(innerType)).AsList()
			: binder.Outputs.GetFields(innerType).ToList();
	}
}