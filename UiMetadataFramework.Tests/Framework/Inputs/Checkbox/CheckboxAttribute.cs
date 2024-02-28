namespace UiMetadataFramework.Tests.Framework.Inputs.Checkbox;

using UiMetadataFramework.Core.Binding;

public class CheckboxAttribute : ConfigurationDataAttribute
{
	[ConfigurationProperty("Style")]
	public string? Style { get; set; }
}