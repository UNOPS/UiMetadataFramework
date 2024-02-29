namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System.Linq;
using FluentAssertions;
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

		[ConfigurationProperty("Format")]
		public string? Format { get; set; }
	}

	[Fact]
	public void DerivedConfigurationIsAllowed()
	{
		var field = this.binder.BuildOutputFields<Response>().Single(t => t.Id == nameof(Response.Money));

		var config = field.Component.ConfigAsDictionary()!;

		Assert.Equal(2, config["DecimalPlaces"]);
		Assert.Equal("en-US", config["Locale"]);
		Assert.Equal("C2", config["Format"]);
	}
}