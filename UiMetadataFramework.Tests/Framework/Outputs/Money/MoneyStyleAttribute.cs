namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using UiMetadataFramework.Core.Binding;

public class MoneyStyleAttribute : ComponentConfigurationAttribute
{
	[ConfigurationProperty("Style")]
	public string? Style { get; set; }
}