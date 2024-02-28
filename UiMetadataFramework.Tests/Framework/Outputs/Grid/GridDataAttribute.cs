namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Core.Binding;

public class GridDataAttribute : ConfigurationDataAttribute
{
	[ConfigurationProperty("Areas")]
	public string? Areas { get; set; }
}