namespace UiMetadataFramework.Tests.Binding.Input.PreConfiguredComponents;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;
using Money = UiMetadataFramework.Tests.Framework.Inputs.Money.Money;

public class ConfigurationOverrides
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Inputs
	{
		[Money(4)]
		public UsdMoney? Usd { get; set; }
	}

	public class UsdMoney : IPreConfiguredComponent<Money>
	{
		[Money(Currency = "USD")]
		public Money? Value { get; set; }
	}

	[Fact]
	public void OuterConfigurationOverridesInner()
	{
		var moneyConfig = this.binder.BuildInputFields<Inputs>()
			.Single(t => t.Id == nameof(Inputs.Usd)).Component.GetConfigurationOrException<MoneyAttribute.Configuration>();

		Assert.Equal(4, moneyConfig.DecimalPlaces);
		Assert.Equal("USD", moneyConfig.Currency);
	}
}