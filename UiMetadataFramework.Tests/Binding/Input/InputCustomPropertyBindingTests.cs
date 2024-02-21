namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Basic.Inputs.DateTime;
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
		var inputFields = this.binder.BuildInputFields<Request>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		inputFields
			.AssertHasInputField(
				nameof(Request.DateOfBirth),
				DateTimeInputComponentBinding.ControlName,
				"DoB",
				orderIndex: 2,
				required: true)
			.HasCustomProperty<IList<object>>(
				"documentation",
				t => t.Cast<string>().Count() == 2,
				"Custom property 'documentation' has incorrect value.");
	}
}