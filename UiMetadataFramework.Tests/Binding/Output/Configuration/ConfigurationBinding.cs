namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class ConfigurationBinding
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[Money(4, Locale = "en-US")]
		public Money? Money { get; set; }

		[Money(4, Locale = "en-US")]
		[MoneyStyleData(Style = "fancy")]
		public Money? StyledMoney { get; set; }
	}

	[Fact]
	public void BindingByMethodCallWorks()
	{
		var field = this.binder.BuildOutputComponent(typeof(Money));

		Assert.NotNull(field.Configuration);

		var component = field.ConfigAsDictionary()!;

		Assert.Equal(10, component["DecimalPlaces"]);
		Assert.Equal("en-UK", component["Locale"]);
	}

	[Fact]
	public void ConfigurationAttributeWorks()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.Money);

		dynamic component = field.ConfigAsDictionary()!;

		Assert.Equal(4, component["DecimalPlaces"]);
		Assert.Equal("en-US", component["Locale"]);
	}

	[Fact]
	public void OptionalConfigurationAttributeWorks()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.StyledMoney);

		dynamic component = field.ConfigAsDictionary()!;

		Assert.Equal(4, component["DecimalPlaces"]);
		Assert.Equal("en-US", component["Locale"]);
		Assert.Equal("fancy", component["Style"]);
	}
}