namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System.Linq;
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
		[MoneyStyleItem(Style = "fancy")]
		public Money? StyledMoney { get; set; }
	}

	[Fact]
	public void BindingByMethodCallWorks()
	{
		var field = this.binder.BindOutputComponent(
			typeof(Money),
			new MoneyAttribute
			{
				DecimalPlaces = 10,
				Locale = "en-UK"
			});

		Assert.NotNull(field.Configuration);

		dynamic component = field.Configuration!;

		Assert.Equal(10, component.DecimalPlaces);
		Assert.Equal("en-UK", component.Locale);
	}

	[Fact]
	public void ConfigurationAttributeWorks()
	{
		var field = this.binder.BindOutputFields<Response>().Single(t => t.Id == nameof(Response.Money));

		dynamic component = field.Component.GetConfigurationOrException();

		Assert.Equal(4, component.DecimalPlaces);
		Assert.Equal("en-US", component.Locale);
	}

	[Fact]
	public void OptionalConfigurationAttributeWorks()
	{
		var field = this.binder.BindOutputFields<Response>().Single(t => t.Id == nameof(Response.StyledMoney));

		dynamic component = field.Component.GetConfigurationOrException();

		Assert.Equal(4, component.DecimalPlaces);
		Assert.Equal("en-US", component.Locale);
		Assert.Equal("fancy", component.Style);
	}
}