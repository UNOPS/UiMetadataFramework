namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Basic.Inputs.DateTime;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InputCustomPropertyBindingTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
	[CustomPropertyConfig(IsArray = true)]
	private sealed class DocumentationAttribute : Attribute, ICustomPropertyAttribute
	{
		public DocumentationAttribute(string text)
		{
			this.Text = text;
		}

		public string Text { get; set; }

		public string Name => "documentation";

		public object GetValue(Type type, MetadataBinder binder)
		{
			return this.Text;
		}
	}

	private sealed class Request
	{
		[Documentation("1")]
		[Documentation("2")]
		[InputField(Label = "DoB", OrderIndex = 2)]
		public DateTime DateOfBirth { get; set; }
	}

	[Fact]
	public void CanBindCustomProperty()
	{
		var inputFields = this.binder.Inputs.GetFields(typeof(Request))
			.Cast<MyInputFieldMetadataFactory.Metadata>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		var field = inputFields
			.AssertHasField<MyInputFieldMetadataFactory.Metadata>(
				nameof(Request.DateOfBirth),
				DateTimeInputComponentBinding.ControlName,
				"DoB",
				orderIndex: 2);

		field.Required.Should().BeTrue();

		field.HasCustomProperty<IList<object>>(
			property: "documentation",
			assertion: t => t.Cast<string>().Count() == 2,
			message: "Custom property 'documentation' has incorrect value.");
	}
}