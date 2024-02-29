namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

public class IconStyleAttribute(string style) : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Style")]
	public string Style { get; set; } = style;
}