// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Input;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Basic.Inputs.Number;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.EventHandlers.Inputs;
using UiMetadataFramework.Tests.Framework.EventHandlers.Outputs;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InputEventHandlerTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private sealed class ValidRequest
	{
		[InputField(Hidden = true)]
		[InputFieldEventHandler]
		public decimal Weight { get; set; }
	}

	private class InvalidRequest
	{
		[OutputFieldEventHandler]
		public string? Name { get; set; }
	}

	[Fact]
	public void CanBindEventHandlers()
	{
		var inputFields = this.binder.Inputs.GetFields(typeof(ValidRequest))
			.Cast<MyInputFieldMetadataFactory.Metadata>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		var field = inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				nameof(ValidRequest.Weight),
				NumberInputComponentBinding.ControlName,
				nameof(ValidRequest.Weight),
				hidden: true,
				eventHandlers: [InputFieldEventHandlerAttribute.Identifier]);
		
		field.Required.Should().BeTrue();
	}

	[Fact]
	public void EventHandlerCanOnlyBeAppliedToIntendedElements()
	{
		Assert.Throws<BindingException>(() => this.binder.Inputs.GetFields(typeof(InvalidRequest)).ToList());
	}
}