// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Flexbox;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InvalidConfigurations
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class BadResponse
	{
		/// <summary>
		/// This property intentionally does not have the [Money] attribute.
		/// </summary>
		public Money? Money { get; set; }
	}

	private class MultipleConfigurationsOfSameType
	{
		[Money(DecimalPlaces = 2, Locale = "en-US")]
		public Money? Money { get; set; }
	}

	private class MultipleConfigurationsOfDifferentTypes
	{
		[Money(DecimalPlaces = 2, Locale = "en-US")]
		[Flexbox]
		public Money? Money { get; set; }
	}

	[Fact]
	public void ExceptionThrowIfMultipleConfigurationsArePresent()
	{
		Assert.Throws<BindingException>(() => this.binder.BindOutputFields<MultipleConfigurationsOfDifferentTypes>().ToList());
	}

	[Fact]
	public void ExceptionThrownIfConfigurationIsMissing()
	{
		Assert.Throws<BindingException>(() => this.binder.BindOutputFields<BadResponse>().ToList());
		Assert.Throws<BindingException>(() => this.binder.BindOutputField(typeof(Money)));
	}

	[Fact]
	public void ExceptionThrownIfConfigurationIsOfWrongType()
	{
		Assert.Throws<BindingException>(() => this.binder.BindOutputField(typeof(Money), new FlexboxAttribute()));
	}
}