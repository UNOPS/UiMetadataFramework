namespace UiMetadataFramework.Tests.Framework.Outputs.Icon;

using UiMetadataFramework.Core.Binding;

public class IconBackgroundAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Background")]
	public string? Color { get; set; }

	/// <summary>
	/// Property that won't be used in the configuration
	/// because it doesn't have [ConfigurationProperty] attribute.
	/// It is used to test that only properties with [ConfigurationProperty]
	/// attribute are actually used when building configuration.
	/// </summary>
	public int IgnoreThisProperty { get; set; }

	[ConfigurationProperty("Pattern")]
	public string? Pattern { get; set; }
}