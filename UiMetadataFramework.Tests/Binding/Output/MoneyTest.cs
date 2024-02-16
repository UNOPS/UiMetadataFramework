// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MoneyTest
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class BadResponse
	{
		public Money? Money { get; set; }
	}

	private class GoodResponse
	{
		[Money(DecimalPlaces = 4, Locale = "en-US")]
		public Money? Money { get; set; }
	}

	[Fact]
	public void CanBuildMetadataFromType()
	{
		var metadata = this.binder.BindOutputField(
			typeof(Money),
			new MoneyAttribute
			{
				DecimalPlaces = 10,
				Locale = "en-UK"
			});

		metadata.HasCustomProperty(MoneyAttribute.PropertyName, t => t.DecimalPlaces == 10, "DecimalPlaces is not set correctly.");
		metadata.HasCustomProperty(MoneyAttribute.PropertyName, t => t.Locale == "en-UK", "Locale is not set correctly.");
	}

	[Fact]
	public void CustomPropertyIncludedInMetadata()
	{
		var field = this.binder.BindOutputFields<GoodResponse>().Single();

		field.HasCustomProperty(MoneyAttribute.PropertyName, t => t.DecimalPlaces == 4, "DecimalPlaces is not set correctly.");
		field.HasCustomProperty(MoneyAttribute.PropertyName, t => t.Locale == "en-US", "Locale is not set correctly.");
	}

	[Fact]
	public void ExceptionThrownIfCustomPropertyIsMissing()
	{
		Assert.Throws<BindingException>(() => this.binder.BindOutputFields<BadResponse>().ToList());
		Assert.Throws<BindingException>(() => this.binder.BindOutputField(typeof(Money)));
	}
}