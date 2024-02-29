namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using UiMetadataFramework.Core.Binding;

public class CheckboxAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Style")]
	public string? Style { get; set; }
}