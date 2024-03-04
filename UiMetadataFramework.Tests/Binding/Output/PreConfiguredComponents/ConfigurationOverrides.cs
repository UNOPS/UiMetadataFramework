namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Framework.Outputs.ObjectList;
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

		[ObjectList(Gap = "10")]
		public BulletPointList<int>? ListOfLists { get; set; }
	}

	[Money(8, Locale = "en-US")]
	[MoneyStyle(Style = "high-precision")]
	public class HighPrecisionMoney : Money;

	[ObjectList(Style = "bullet-point-list")]
	public class BulletPointList<T> : ObjectList<T>;

	[Fact]
	public void MultilevelOuterConfigurations()
	{
		var config = this.binder
			.BuildOutputComponent<Outputs>(t => t.ListOfLists)
			.ConfigAsDictionary()!;

		Assert.Equal("10", config["Gap"]);
		Assert.Equal("bullet-point-list", config["Style"]);
		Assert.False(config.ContainsKey("ListItem"));
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