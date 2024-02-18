namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using System;
using UiMetadataFramework.Core.Binding;

public class CheckboxAttribute : ComponentConfigurationAttribute
{
	public string? Style { get; set; }

	public override object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] additionalConfigurations)
	{
		return new Configuration { Style = this.Style };
	}

	public class Configuration
	{
		public string? Style { get; set; }
	}
}