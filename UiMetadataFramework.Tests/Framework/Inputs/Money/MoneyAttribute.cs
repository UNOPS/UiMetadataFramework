namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using System;
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

	public string? Currency { get; set; }
	public int? DecimalPlaces { get; set; }

	public override object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ConfigurationDataAttribute[] configurationData)
	{
		return new Configuration(this.Currency, this.DecimalPlaces);
	}

	public class Configuration(string? currency, int? decimalPlaces)
	{
		public string? Currency { get; } = currency;
		public int? DecimalPlaces { get; } = decimalPlaces;
	}
}