namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using System;
using System.Linq;
using UiMetadataFramework.Core.Binding;

public class MoneyAttribute : ComponentConfigurationAttribute
{
	public const string PropertyName = "money";

	public int DecimalPlaces { get; set; } = 2;
	public string? Locale { get; set; }

	public override object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] additionalConfigurations)
	{
		var style = additionalConfigurations.OfType<MoneyStyleItemAttribute>().SingleOrDefault()?.Style;

		return new
		{
			this.DecimalPlaces,
			this.Locale,
			Style = style
		};
	}
}