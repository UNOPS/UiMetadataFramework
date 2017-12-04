namespace UiMetadataFramework.Tests.Binding
{
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public partial class BinderTests
	{
		[Fact]
		public void CanGetFormMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			binder.BindForm<DoMagic, Request, Response>();
		}

		[Fact]
		public void CanGetInputFieldsMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var inputFields = binder.BindInputFields<Request>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(10, inputFields.Count);
			inputFields.AssertHasInputField(nameof(Request.FirstName), StringInputFieldBinding.ControlName, "First name", orderIndex: 1, required: true);
			inputFields.AssertHasInputField(nameof(Request.DateOfBirth), DateTimeInputFieldBinding.ControlName, "DoB", orderIndex: 2, required: true);
			inputFields.AssertHasInputField(nameof(Request.SubmissionDate), DateTimeInputFieldBinding.ControlName, nameof(Request.SubmissionDate));
			inputFields.AssertHasInputField(nameof(Request.Notes), TextareaInputFieldBinding.ControlName, nameof(Request.Notes));

			inputFields.AssertHasInputField(nameof(Request.Height), NumberInputFieldBinding.ControlName, nameof(Request.Height), hidden: true);

			inputFields.AssertHasInputField(nameof(Request.Weight), NumberInputFieldBinding.ControlName, nameof(Request.Weight), hidden: true, required: true,
				eventHandlers: new[] { InputFieldEventHandlerAttribute.Identifier });

			inputFields.AssertHasInputField(nameof(Request.IsRegistered), BooleanInputFieldBinding.ControlName, nameof(Request.IsRegistered), required: true);

			inputFields.AssertHasInputField(nameof(Request.Day), DropdownInputFieldBinding.ControlName, nameof(Request.Day))
				.HasCustomProperties<DropdownProperties>(t => t.Items.Count == 7, "Dropdown has incorrect number of items.");

			inputFields.AssertHasInputField(nameof(Request.FirstDayOfWeek), DropdownInputFieldBinding.ControlName, nameof(Request.FirstDayOfWeek))
				.HasCustomProperties<DropdownProperties>(t => t.Items.Count == 2, "Dropdown has incorrect number of items.");

			inputFields.AssertHasInputField(nameof(Request.Category), DropdownInputFieldBinding.ControlName, nameof(Request.Category))
				.HasCustomProperties<DropdownProperties>(t => t.Items.Count == 3, "Dropdown has incorrect number of items.");
		}

		[Fact]
		public void CanGetOutputFieldsMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			binder.RegisterAssembly(typeof(BinderTests).GetTypeInfo().Assembly);

			var outputFields = binder.BindOutputFields<Response>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(6, outputFields.Count);
			outputFields.AssertHasOutputField(nameof(Response.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			outputFields.AssertHasOutputField(nameof(Response.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2);
			outputFields.AssertHasOutputField(nameof(Response.Height), NumberOutputFieldBinding.ControlName, nameof(Response.Height), true);
			outputFields.AssertHasOutputField(nameof(Response.Weight), NumberOutputFieldBinding.ControlName, nameof(Response.Weight), true, 0,
				new[] { OutputFieldEventHandlerAttribute.Identifier });
			outputFields.AssertHasOutputField(nameof(Response.OtherPeople), MetadataBinder.ObjectListOutputControlName, nameof(Response.OtherPeople));
			outputFields.AssertHasOutputField(nameof(Response.Categories), MetadataBinder.ValueListOutputControlName, nameof(Response.Categories));

			var ienumerableProperty = outputFields.Single(t => t.Id == nameof(Response.OtherPeople));

			var columns = ((EnumerableOutputFieldProperties)ienumerableProperty.CustomProperties).Columns;
			columns.AssertHasOutputField(nameof(Person.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			columns.AssertHasOutputField(nameof(Person.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2);
			columns.AssertHasOutputField(nameof(Person.Height), NumberOutputFieldBinding.ControlName, nameof(Person.Height), true);
			columns.AssertHasOutputField(nameof(Person.Weight), NumberOutputFieldBinding.ControlName, nameof(Person.Weight), true);
		}

		[Fact]
		public void DuplicateAttemptsToBindSameAssemblyAreIgnored()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
		}

		[Fact]
		public void EventHandlerCanOnlyBeAppliedToIntendedElements()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			binder.RegisterAssembly(typeof(BinderTests).GetTypeInfo().Assembly);

			Assert.Throws<BindingException>(() => binder.BindInputFields<InvalidRequest>().ToList());
			Assert.Throws<BindingException>(() => binder.BindOutputFields<InvalidResponse>().ToList());
		}
	}
}