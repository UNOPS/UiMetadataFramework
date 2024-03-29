﻿namespace UiMetadataFramework.Tests.Framework.Outputs.Custom;

using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class CustomOutputFieldAttribute : OutputFieldAttribute
{
	public string? Style { get; set; }

	public override OutputFieldMetadata GetMetadata(
		PropertyInfo property,
		OutputComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		return new Metadata(basic) { Style = this.Style };
	}

	public class Metadata(OutputFieldMetadata basic) : OutputFieldMetadata(basic)
	{
		public string? Style { get; set; }
	}
}