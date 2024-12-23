namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Provides binding for all <see cref="IEnumerable{T}"/> properties.
/// </summary>
public class TableOutputComponentBinding : OutputComponentBinding
{
	/// <inheritdoc />
	public TableOutputComponentBinding() : base(
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