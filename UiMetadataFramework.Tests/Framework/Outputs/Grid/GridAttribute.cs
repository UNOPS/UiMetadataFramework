namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Core.Binding;

public class GridAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Areas")]
	public string? Areas { get; set; }
}