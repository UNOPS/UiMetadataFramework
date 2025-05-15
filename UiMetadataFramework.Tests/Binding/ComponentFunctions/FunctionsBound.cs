namespace UiMetadataFramework.Tests.Binding.ComponentFunctions;

using System.Collections.Generic;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class FunctionsBound
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void GetBindings()
	{
		var binding = this.binder.Inputs.Bindings.GetBinding(typeof(Money));

		binding.Functions.Length.Should().Be(1);

		var parameters = binding.Functions[0].Method.GetParameters();
		parameters.Length.Should().Be(1);
		parameters[0].Name.Should().Be("amount");

		Assert.NotNull(binding);
	}

	[Fact]
	public void CanInvokeFunction()
	{
		var binding = this.binder.Inputs.Bindings.GetBinding(typeof(Money));

		var sp = ServiceProviderFactory.CreateServiceProvider();
		
		var result = binding.RunFunction(
			nameof(Money.Half),
			new Dictionary<string, object?> { { "amount", new Money { Amount = 100 } } },
			sp);
		
		result.Should().Be(50m);
	}
}