namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class ConfigurationBinding
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class GoodResponse
	{
		[Money(DecimalPlaces = 4, Locale = "en-US")]
		public Money? Money { get; set; }
	}

	[Fact]
	public void BindingByMethodCallWorks()
	{
		var field = this.binder.BindOutputField(
			typeof(Money),
			new MoneyAttribute
			{
				DecimalPlaces = 10,
				Locale = "en-UK"
			});

		Assert.NotNull(field.Configuration);

		dynamic component = field.Configuration!;

		Assert.Equal(10, component!.DecimalPlaces);
		Assert.Equal("en-UK", component.Locale);
	}

	[Fact]
	public void ConfigurationAttributeWorks()
	{
		var field = this.binder.BindOutputFields<GoodResponse>().Single();

		dynamic component = field.GetComponentConfigurationOrException();

		Assert.Equal(4, component.DecimalPlaces);
		Assert.Equal("en-US", component.Locale);
	}
}