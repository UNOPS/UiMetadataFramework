namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

public class IconColorAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Color")]
	public string? Color { get; set; }
}