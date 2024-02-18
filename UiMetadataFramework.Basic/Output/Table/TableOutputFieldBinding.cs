namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Provides binding for all <see cref="IEnumerable{T}"/> properties.
/// </summary>
public class TableOutputFieldBinding : OutputFieldBinding
{
	/// <summary>
	/// Name of the client-side control which should be able to render tabular data.
	/// </summary>
	public const string ObjectListOutputControlName = "table";

	/// <inheritdoc />
	public TableOutputFieldBinding() : base(
		new[]
		{
			typeof(IEnumerable<>),
			typeof(IList<>),
			typeof(Array)
		},
		ObjectListOutputControlName,
		typeof(TableMetadataFactory))
	{
	}
}