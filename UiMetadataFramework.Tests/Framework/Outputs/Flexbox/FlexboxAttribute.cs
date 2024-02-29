namespace UiMetadataFramework.Tests.Framework.Outputs.Flexbox;

using UiMetadataFramework.Core.Binding;

public class FlexboxAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Style")]
	public string? Style { get; set; }
}