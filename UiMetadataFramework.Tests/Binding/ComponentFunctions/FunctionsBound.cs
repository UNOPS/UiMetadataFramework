namespace UiMetadataFramework.Tests.Binding.ComponentFunctions;

using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UiMetadataFramework.Basic.Server;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.App.Inputs;
using UiMetadataFramework.Tests.Framework.Inputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class FunctionsBound
{
	private readonly IServiceProvider sp = ServiceProviderFactory.CreateServiceProvider();

	[Fact]
	public void CanInvokeFunction()
	{
		var runner = this.sp.GetRequiredService<ComponentFunctionRunner>();

		var result = runner.RunFunction(
			typeof(Money).FullName!,
			"half",
			new Dictionary<string, object?> { { "amount", new Money { Amount = 100 } } },
			this.sp);

		result.Should().Be(50m + typeof(MetadataBinder).ToString());
	}

	[Fact]
	public void CanUseInDerivedComponent()
	{
		var runner = this.sp.GetRequiredService<ComponentFunctionRunner>();

		var result = runner.RunFunction(
			typeof(AddressInput).FullName!,
			"get-description",
			new Dictionary<string, object?>
			{
				{
					"address", new AddressInput
					{
						City = "Tokyo",
						Country = "Japan"
					}
				}
			},
			this.sp);

		result.Should().Be("Tokyo, Japan");
	}
}