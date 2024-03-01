// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.EventHandlers.Inputs;
using UiMetadataFramework.Tests.Framework.EventHandlers.Outputs;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class EventHandlerTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class InvalidResponse
	{
		[InputFieldEventHandler]
		public string? Name { get; set; }
	}

	private class ValidResponse
	{
		[OutputFieldEventHandler]
		public decimal Weight { get; set; }
	}

	[Fact]
	public void CanBindEventHandlers()
	{
		var outputFields = this.binder.Outputs
			.GetFields(typeof(ValidResponse))
			.OrderBy(t => t.OrderIndex)
			.ToList();

		var field = outputFields.AssertHasOutputField(nameof(ValidResponse.Weight));

		field.EventHandlers.Should().NotBeNull();
		field.EventHandlers.Should().HaveCount(1);
		field.EventHandlers![0].Id.Should().Be(OutputFieldEventHandlerAttribute.Identifier);
	}

	[Fact]
	public void EventHandlerCanOnlyBeAppliedToIntendedElements()
	{
		Assert.Throws<BindingException>(() => this.binder.Outputs.GetFields(typeof(InvalidResponse)).ToList());
	}
}