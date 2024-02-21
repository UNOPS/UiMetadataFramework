namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System;
using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DerivedConfiguration
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[BetterMoney(2, Locale = "en-US", Format = "C2")]
		public Money? Money { get; set; }
	}

	private class BetterMoney : MoneyAttribute
	{
		// ReSharper disable once UnusedMember.Local
		public BetterMoney()
		{
		}

		public BetterMoney(int decimalPlaces)
		{
			this.DecimalPlaces = decimalPlaces;
		}

		public string? Format { get; set; }

		public override object CreateMetadata(
			Type type,
			MetadataBinder binder,
			params ComponentConfigurationItemAttribute[] configurationItems)
		{
			return new BetterConfiguration
			{
				DecimalPlaces = this.DecimalPlaces,
				Locale = this.Locale,
				Format = this.Format
			};
		}

		public class BetterConfiguration : Configuration
		{
			public string? Format { get; set; }
		}
	}

	[Fact]
	public void DerivedConfigurationIsAllowed()
	{
		var field = this.binder.BuildOutputFields<Response>().Single(t => t.Id == nameof(Response.Money));

		dynamic component = field.Component.GetConfigurationOrException();

		Assert.Equal(2, component.DecimalPlaces);
		Assert.Equal("en-US", component.Locale);
		Assert.Equal("C2", component.Format);
	}
}