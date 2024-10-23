namespace UiMetadataFramework.Tests.Framework.Outputs;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

public class MyOutputComponentAttribute(string name, Type? metadataFactory = null) : OutputComponentAttribute(name, metadataFactory)
{
	public int DefaultOrderIndex { get; set; }

	/// <inheritdoc cref="OutputComponentAttribute.GetAdditionalData"/>
	public override IReadOnlyDictionary<string, object?> GetAdditionalData()
	{
		return new Dictionary<string, object?> { { nameof(this.DefaultOrderIndex), this.DefaultOrderIndex } };
	}
}