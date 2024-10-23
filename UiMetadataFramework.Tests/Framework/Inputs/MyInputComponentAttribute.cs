namespace UiMetadataFramework.Tests.Framework.Inputs;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core.Binding;

public class MyInputComponentAttribute(string name, Type? metadataFactory = null) : InputComponentAttribute(name, metadataFactory)
{
	public string? DefaultLabel { get; set; }

	public override IReadOnlyDictionary<string, object?> GetAdditionalData()
	{
		return new Dictionary<string, object?> { { nameof(this.DefaultLabel), this.DefaultLabel } };
	}
}