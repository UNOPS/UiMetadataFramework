namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

public class IconColorDataAttribute : ConfigurationDataAttribute
{
	[ConfigurationProperty("Color")]
	public string? Color { get; set; }
}