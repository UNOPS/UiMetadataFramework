namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using System;
using UiMetadataFramework.Core.Binding;

public class FlexboxAttribute() : CustomPropertyAttribute(PropertyName)
{
	public const string PropertyName = "flexbox";

	public string? Style { get; set; }

	public override object GetValue(Type type, MetadataBinder binder)
	{
		return new { this.Style };
	}
}