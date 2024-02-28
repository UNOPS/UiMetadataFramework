namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

public class IconStyleDataAttribute(string style) : ConfigurationDataAttribute
{
	[ConfigurationProperty("Style")]
	public string Style { get; set; } = style;
}