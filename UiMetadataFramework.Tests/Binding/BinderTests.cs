namespace UiMetadataFramework.Tests.Binding
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public partial class BinderTests
	{
		[Fact]
		public void CanGetFormMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var formMetadata = binder.BindForm<DoMagic, Request, Response>();

			formMetadata
				.HasCustomProperty("style", "blue")
				.HasCustomProperty("number", 1_001);

			var docs = ((List<object>)formMetadata.CustomProperties["documentation"]).Cast<string>().ToList();
			Assert.True(docs.Count == 2);
			Assert.True(docs[0] == "help 1");
			Assert.True(docs[1] == "help 2");
		}

		[Fact]
		public void CanGetInputFieldsMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var inputFields = binder.BindInputFields<Request>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(11, inputFields.Count);
			inputFields.AssertHasInputField(nameof(Request.FirstName), StringInputFieldBinding.ControlName, "First name", orderIndex: 1, required: true);
			inputFields.AssertHasInputField(nameof(Request.DateOfBirth), DateTimeInputFieldBinding.ControlName, "DoB", orderIndex: 2, required: true);
			inputFields.AssertHasInputField(nameof(Request.SubmissionDate), DateTimeInputFieldBinding.ControlName, nameof(Request.SubmissionDate));
			inputFields.AssertHasInputField(nameof(Request.Height), NumberInputFieldBinding.ControlName, nameof(Request.Height), hidden: true);

			inputFields.AssertHasInputField(nameof(Request.Notes), TextareaInputFieldBinding.ControlName, nameof(Request.Notes))
				.HasCustomProperty("number-1", 1)
				.HasCustomProperty("number-2", 2);

			inputFields.AssertHasInputField(nameof(Request.Weight), NumberInputFieldBinding.ControlName, nameof(Request.Weight), hidden: true, required: true,
				eventHandlers: new[] { InputFieldEventHandlerAttribute.Identifier });

			inputFields.AssertHasInputField(nameof(Request.IsRegistered), BooleanInputFieldBinding.ControlName, nameof(Request.IsRegistered), required: true);

			inputFields.AssertHasInputField(nameof(Request.Day), DropdownInputFieldBinding.ControlName, nameof(Request.Day))
				.HasCustomProperty<IList<DropdownItem>>("Items", t => t.Count == 7, "Dropdown has incorrect number of items.");

			inputFields.AssertHasInputField(nameof(Request.FirstDayOfWeek), DropdownInputFieldBinding.ControlName, nameof(Request.FirstDayOfWeek))
				.HasCustomProperty<IList<DropdownItem>>("Items", t => t.Count == 2, "Dropdown has incorrect number of items.")
				.HasCustomProperty("secret", "password");

			inputFields.AssertHasInputField(nameof(Request.Category), DropdownInputFieldBinding.ControlName, nameof(Request.Category))
				.HasCustomProperty<IList<DropdownItem>>("Items", t => t.Count == 3, "Dropdown has incorrect number of items.")
				.HasCustomProperty<IList<object>>("documentation", t => t.Cast<string>().Count() == 2, "Custom property 'documentation' has incorrect value.");
		}

		[Fact]
		public void CanGetOutputFieldsMetadata()
		{
			var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			binder.RegisterAssembly(typeof(BinderTests).GetTypeInfo().Assembly);

			var outputFields = binder.BindOutputFields<Response>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(7, outputFields.Count);
			outputFields.AssertHasOutputField(nameof(Response.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			outputFields.AssertHasOutputField(nameof(Response.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2)
				.HasCustomProperty("style", "beatiful")
				.HasCustomProperty("secret", 123);

			outputFields.AssertHasOutputField(nameof(Response.Height), NumberOutputFieldBinding.ControlName, nameof(Response.Height), true);
			outputFields.AssertHasOutputField(nameof(Response.Weight), NumberOutputFieldBinding.ControlName, nameof(Response.Weight), true, 0,
					new[] { OutputFieldEventHandlerAttribute.Identifier })
				.HasCustomProperty("help", "this is help text")
				.HasCustomProperty("number", 456);

			outputFields.AssertHasOutputField(nameof(Response.OtherPeople), MetadataBinder.ObjectListOutputControlName, nameof(Response.OtherPeople))
				.HasCustomProperty("style", "cool")
				.HasCustomProperty("secret", 321)
				.HasCustomProperty<IList<object>>("documentation", t => t.Cast<string>().Count() == 2, "Custom property 'documentation' has incorrect value.");

			outputFields.AssertHasOutputField(nameof(Response.Categories), MetadataBinder.ValueListOutputControlName, nameof(Response.Categories));

			outputFields.AssertHasOutputField(nameof(Response.MainPeople), "paginated-data", nameof(Response.MainPeople))
				.HasCustomProperty("style", "main");

			var ienumerableProperty = outputFields.Single(t => t.Id == nameof(Response.OtherPeople));

			Assert.True(ienumerableProperty.CustomProperties.ContainsKey("Customizations"));
			var columns = ienumerableProperty.CustomProperties["Columns"] as IList<OutputFieldMetadata>;
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