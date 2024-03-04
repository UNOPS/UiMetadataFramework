namespace UiMetadataFramework.Tests.Binding.Input.PreConfiguredComponents;

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

	[Money(Currency = "USD")]
	public class UsdMoney : Money;

	[Fact]
	public void OuterConfigurationOverridesInner()
	{
		var config = this.binder
			.BuildInputComponent<Inputs>(t => t.Usd)
			.ConfigAsDictionary()!;

		Assert.Equal(4, config["DecimalPlaces"]);
		Assert.Equal("USD", config["Currency"]);
	}
}