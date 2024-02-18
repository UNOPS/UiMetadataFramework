namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using System;
using UiMetadataFramework.Core.Binding;

public class FlexboxAttribute : ComponentConfigurationAttribute
{
	public string? Style { get; set; }

	public override object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] additionalConfigurations)
	{
		return new { this.Style };
	}
}