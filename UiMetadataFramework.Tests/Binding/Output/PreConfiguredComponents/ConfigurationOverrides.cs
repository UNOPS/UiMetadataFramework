namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class ConfigurationOverrides
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Outputs
	{
		[Money(Locale = "en-EN")]
		[MoneyStyle(Style = "high-precision-override")]
		public HighPrecisionMoney? Amounts { get; set; }
	}

	public class HighPrecisionMoney : IPreConfiguredComponent<Money>
	{
		[Money(8, Locale = "en-US")]
		[MoneyStyle(Style = "high-precision")]
		public Money? Value { get; set; }
	}

	[Fact]
	public void OuterConfigurationOverridesInner()
	{
		var moneyConfig = this.binder
			.BuildOutputComponent<Outputs>(t => t.Amounts)
			.ConfigAsDictionary()!;

		Assert.Equal(8, moneyConfig["DecimalPlaces"]);
		Assert.Equal("en-EN", moneyConfig["Locale"]);
		Assert.Equal("high-precision-override", moneyConfig["Style"]);
	}
}