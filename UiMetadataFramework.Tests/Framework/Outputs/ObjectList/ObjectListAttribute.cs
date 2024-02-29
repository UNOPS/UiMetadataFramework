namespace UiMetadataFramework.Tests.Framework.Outputs.ObjectList;

using UiMetadataFramework.Core.Binding;

public class ObjectListAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Gap")]
	public string? Gap { get; set; }

	[ConfigurationProperty("ListItem")]
	public string? ListItem { get; set; }

	[ConfigurationProperty("Style")]
	public string? Style { get; set; }
}