namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using UiMetadataFramework.Core.Binding;

public class MoneyAttribute : ComponentConfigurationAttribute
{
	public MoneyAttribute()
	{
	}

	public MoneyAttribute(int decimalPlaces)
	{
		this.DecimalPlaces = decimalPlaces;
	}

	[ConfigurationProperty("Currency")]
	public string? Currency { get; set; }

	[ConfigurationProperty("DecimalPlaces")]
	public int? DecimalPlaces { get; set; }
}