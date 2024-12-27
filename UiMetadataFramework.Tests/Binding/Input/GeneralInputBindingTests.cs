namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Basic.Inputs.DateTime;
using UiMetadataFramework.Basic.Inputs.Number;
using UiMetadataFramework.Basic.Inputs.Text;
using UiMetadataFramework.Basic.Inputs.Textarea;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class GeneralInputBindingTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
		[InputField(Label = "First name", OrderIndex = 1, Required = true)]
		public string? FirstName { get; set; }

		[InputField(Hidden = true)]
		public int? Height { get; set; }

		[IntProperty("number-1", 1)]
		[IntProperty("number-2", 2)]
		public TextareaValue? Notes { get; set; }

		public DateTime? SubmissionDate { get; set; }
	}

	[Fact]
	public void CanGetInputFieldsMetadata()
	{
		var inputFields = this.binder.Inputs
			.GetFields(typeof(Request))
			.Cast<MyInputFieldMetadataFactory.Metadata>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		Assert.Equal(4, inputFields.Count);

		inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				nameof(Request.FirstName),
				StringInputComponentBinding.ControlName,
				"First name",
				orderIndex: 1)
			.Required.Should().BeTrue();

		inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				nameof(Request.SubmissionDate),
				DateTimeInputComponentBinding.ControlName,
				nameof(Request.SubmissionDate));

		inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				id: nameof(Request.Height),
				type: NumberInputComponentBinding.ControlName,
				label: nameof(Request.Height),
				hidden: true);

		inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				nameof(Request.Notes),
				TextareaValue.ControlName,
				nameof(Request.Notes))
			.HasCustomProperty("number-1", 1)
			.HasCustomProperty("number-2", 2);
	}
}