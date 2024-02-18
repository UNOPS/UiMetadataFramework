namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System;
using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DerivedConfiguration
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[BetterMoney(DecimalPlaces = 2, Locale = "en-US", Format = "C2")]
		public Money? Money { get; set; }
	}

	private class BetterMoney : MoneyAttribute
	{
		public string Format { get; set; }

		public override object CreateMetadata(Type type, MetadataBinder binder)
		{
			return new
			{
				this.DecimalPlaces,
				this.Locale,
				this.Format
			};
		}
	}

	[Fact]
	public void DerivedConfigurationIsAllowed()
	{
		var field = this.binder.BindOutputFields<Response>().Single();

		dynamic component = field.GetComponentConfigurationOrException();

		Assert.Equal(2, component.DecimalPlaces);
		Assert.Equal("en-US", component.Locale);
		Assert.Equal("C2", component.Format);
	}
}