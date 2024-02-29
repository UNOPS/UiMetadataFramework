// ReSharper disable MemberCanBeProtected.Global

namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using UiMetadataFramework.Core.Binding;

public class MoneyAttribute : ComponentConfigurationAttribute
{
	public MoneyAttribute()
	{
	}

	public MoneyAttribute(int decimals)
	{
		this.DecimalPlaces = decimals;
	}

	[ConfigurationProperty("DecimalPlaces")]
	public int? DecimalPlaces { get; set; }

	[ConfigurationProperty("Locale")]
	public string? Locale { get; set; }
}