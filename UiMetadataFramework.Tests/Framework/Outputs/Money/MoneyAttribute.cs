namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using System;
using System.Linq;
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

	public int? DecimalPlaces { get; set; }
	public string? Locale { get; set; }

	public override object CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] configurationItems)
	{
		var style = configurationItems.OfType<MoneyStyleItemAttribute>().FirstOrDefault()?.Style;

		return new Configuration
		{
			DecimalPlaces = this.DecimalPlaces,
			Locale = this.Locale,
			Style = style
		};
	}

	public class Configuration
	{
		public int? DecimalPlaces { get; set; }
		public string? Locale { get; set; }
		public string? Style { get; set; }
	}
}