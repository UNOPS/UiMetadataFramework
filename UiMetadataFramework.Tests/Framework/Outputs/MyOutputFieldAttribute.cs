namespace UiMetadataFramework.Tests.Framework.Outputs;

using System.Collections.Generic;
using System.Reflection;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class MyOutputFieldAttribute : OutputFieldAttribute
{
	public string? Style { get; set; }

	public override OutputFieldMetadata GetMetadata(
		PropertyInfo property,
		OutputComponentBinding binding,
		MetadataBinder binder)
	{
		var basic = base.GetMetadata(property, binding, binder);

		if (binding.AdditionalData?.GetValueOrDefault(nameof(MyOutputComponentAttribute.DefaultOrderIndex)) is int defaultOrderIndex)
		{
			basic.OrderIndex = this.OrderIndex == 0
				? defaultOrderIndex
				: this.OrderIndex;
		}

		return new Metadata(basic) { Style = this.Style };
	}

	public class Metadata(OutputFieldMetadata basic) : OutputFieldMetadata(basic)
	{
		public string? Style { get; set; }
	}
}