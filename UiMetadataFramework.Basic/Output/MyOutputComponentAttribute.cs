namespace UiMetadataFramework.Basic.Output;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyOutputComponentAttribute : ComponentAttribute
{
	/// <inheritdoc />
	public MyOutputComponentAttribute(string name, Type? metadataFactory = null) : base(
		MetadataBinder.ComponentCategories.Output,
		name,
		metadataFactory)
	{
	}

	/// <summary>
	/// Default order index.
	/// </summary>
	public int DefaultOrderIndex { get; set; }

	/// <summary>
	/// If true then the output field won't have a label, unless one is explicitly specified
	/// by `<see cref="OutputFieldAttribute"/>.<see cref="FieldAttribute.Label"/>`.
	/// </summary>
	public bool NoLabelByDefault { get; set; }

	/// <inheritdoc />
	public override IReadOnlyDictionary<string, object?> GetAdditionalData()
	{
		return new Dictionary<string, object?>
		{
			{ nameof(this.NoLabelByDefault), this.NoLabelByDefault },
			{ nameof(this.DefaultOrderIndex), this.DefaultOrderIndex }
		};
	}
}