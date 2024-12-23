namespace UiMetadataFramework.Basic.Output;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputComponentAttribute : OutputComponentAttribute
{
	/// <inheritdoc />
	public MyOutputComponentAttribute(string name, Type? metadataFactory = null) : base(name, metadataFactory)
	{
	}

	/// <summary>
	/// If true then the output field won't have a label, unless one is explicitly specified
	/// by `<see cref="OutputFieldAttribute"/>.<see cref="OutputFieldAttribute.Label"/>`.
	/// </summary>
	public bool NoLabelByDefault { get; set; }

	/// <inheritdoc />
	public override IReadOnlyDictionary<string, object?> GetAdditionalData()
	{
		return new Dictionary<string, object?> { { nameof(NoLabelByDefault), this.NoLabelByDefault } };
	}
}