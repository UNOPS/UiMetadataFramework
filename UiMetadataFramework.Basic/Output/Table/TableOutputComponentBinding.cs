namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Provides binding for all <see cref="IEnumerable{T}"/> properties.
/// </summary>
public class TableOutputComponentBinding : ComponentBinding
{
	/// <inheritdoc />
	public TableOutputComponentBinding() : base(
		MetadataBinder.ComponentCategories.Output,
		[
			typeof(IEnumerable<>),
			typeof(IList<>),
			typeof(Array)
		],
		"table",
		typeof(TableMetadataFactory))
	{
		this.AdditionalData = new Dictionary<string, object?> { { nameof(MyOutputComponentAttribute.NoLabelByDefault), true } };
	}
}