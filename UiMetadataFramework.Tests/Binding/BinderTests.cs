namespace UiMetadataFramework.Tests.Binding
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic;
	using UiMetadataFramework.Basic.Inputs.Boolean;
	using UiMetadataFramework.Basic.Inputs.DateTime;
	using UiMetadataFramework.Basic.Inputs.Dropdown;
	using UiMetadataFramework.Basic.Inputs.Number;
	using UiMetadataFramework.Basic.Inputs.Text;
	using UiMetadataFramework.Basic.Inputs.Textarea;
	using UiMetadataFramework.Basic.Output.DateTime;
	using UiMetadataFramework.Basic.Output.Number;
	using UiMetadataFramework.Basic.Output.Text;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public partial class BinderTests
	{
		public BinderTests()
		{
			this.binder = new MetadataBinder(new DefaultDependencyInjectionContainer());
			this.binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			this.binder.RegisterAssembly(typeof(BinderTests).GetTypeInfo().Assembly);
		}

		private readonly MetadataBinder binder;

		[Theory]
		[InlineData(nameof(Response.Links))]
		[InlineData(nameof(Response.Categories))]
		public void EnumerableOfKnownOutputTypeIsBoundToList(string property)
		{
			var outputField = this.binder.BindOutputFields<Response>().Single(t => t.Id == property);

			Assert.Equal(MetadataBinder.ValueListOutputControlName, outputField.Type);
		}

		[Theory]
		[InlineData(nameof(Response.Links), "formlink")]
		[InlineData(nameof(Response.Categories), StringInputFieldBinding.ControlName)]
		public void EnumerableOfKnownOutputTypeIndicatesType(string property, string itemType)
		{
			var outputField = this.binder.BindOutputFields<Response>().Single(t => t.Id == property);

			outputField.HasCustomProperty<string>("Type", t => t == itemType);
		}

		[Fact]
		public void CanBindDerivedInputFieldAttribute()
		{
			var inputField = this.binder
				.BindInputFields<Request>()
				.Single(t => t.Id == nameof(Request.IsRegistered));

			var custom = inputField as CustomInputFieldAttribute.Metadata;

			Assert.NotNull(custom);
			Assert.Equal("fancy", custom!.Style);
		}

		[Fact]
		public void CanBindDerivedOutputFieldAttribute()
		{
			var outputField = this.binder
				.BindOutputFields<Response>()
				.Single(t => t.Id == nameof(Response.Weight));

			var custom = outputField as CustomOutputFieldAttribute.Metadata;

			Assert.NotNull(custom);
			Assert.Equal("fancy-output", custom!.Style);
		}

		[Fact]
		public void CanGetFormMetadata()
		{
			var formMetadata = this.binder.BindForm<DoMagic, Request, Response>();

			formMetadata
				.HasCustomProperty("style", "blue")
				.HasCustomProperty("number", 1_001)
				.HasCustomProperty("bool-false", false)
				.HasCustomProperty("bool-true", true);

			var docs = ((List<object>)formMetadata.CustomProperties!["documentation"]!).Cast<string>().ToList();
			Assert.True(docs.Count == 2);
			Assert.True(docs[0] == "help 1");
			Assert.True(docs[1] == "help 2");
		}

		[Fact]
		public void CanGetInputFieldsMetadata()
		{
			var inputFields = this.binder.BindInputFields<Request>()
				.OrderBy(t => t.OrderIndex)
				.ToList();

			Assert.Equal(10, inputFields.Count);

			inputFields
				.AssertHasInputField(
					nameof(Request.FirstName),
					StringInputFieldBinding.ControlName,
					"First name",
					orderIndex: 1,
					required: true);

			inputFields
				.AssertHasInputField(
					nameof(Request.DateOfBirth),
					DateTimeInputFieldBinding.ControlName,
					"DoB",
					orderIndex: 2,
					required: true)
				.HasCustomProperty<IList<object>>(
					"documentation",
					t => t.Cast<string>().Count() == 2,
					"Custom property 'documentation' has incorrect value.");

			inputFields
				.AssertHasInputField(
					nameof(Request.SubmissionDate),
					DateTimeInputFieldBinding.ControlName,
					nameof(Request.SubmissionDate));

			inputFields
				.AssertHasInputField(
					nameof(Request.Height),
					NumberInputFieldBinding.ControlName,
					nameof(Request.Height),
					hidden: true);

			inputFields
				.AssertHasInputField(nameof(Request.Notes), TextareaValue.ControlName, nameof(Request.Notes))
				.HasCustomProperty("number-1", 1)
				.HasCustomProperty("number-2", 2);

			inputFields
				.AssertHasInputField(
					nameof(Request.Weight),
					NumberInputFieldBinding.ControlName,
					nameof(Request.Weight),
					hidden: true,
					required: true,
					eventHandlers: new[] { InputFieldEventHandlerAttribute.Identifier });

			inputFields
				.AssertHasInputField(
					nameof(Request.IsRegistered),
					BooleanInputFieldBinding.ControlName,
					nameof(Request.IsRegistered),
					required: true);

			inputFields
				.AssertHasInputField(nameof(Request.Day), DropdownValue<int>.ControlName, nameof(Request.Day))
				.HasCustomProperty<IList<DropdownItem>>("Items", t => t.Count == 7, "Dropdown has incorrect number of items.")
				.HasCustomProperty("secret", "password");

			inputFields
				.AssertHasInputField(
					nameof(Request.Gender),
					DropdownValue<int>.ControlName,
					nameof(Request.Gender));

			var dropdownInputField = inputFields.Single(t => t.Id == nameof(Request.Gender));
			var items = dropdownInputField.CustomProperties?["Items"] as IEnumerable<DropdownItem>;

			Assert.NotNull(items);
			Assert.Equal(2, items!.Count());
		}

		[Fact]
		public void CanGetOutputFieldsMetadata()
		{
			var outputFields = this.binder.BindOutputFields<Response>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(8, outputFields.Count);

			outputFields
				.AssertHasOutputField(
					nameof(Response.FirstName),
					StringOutputFieldBinding.ControlName,
					"First name",
					false,
					1);

			outputFields
				.AssertHasOutputField(
					nameof(Response.DateOfBirth),
					DateTimeOutputFieldBinding.ControlName,
					"DoB",
					false,
					2)
				.HasCustomProperty("style", "beatiful")
				.HasCustomProperty("secret", 123);

			outputFields
				.AssertHasOutputField(
					nameof(Response.Height),
					NumberOutputFieldBinding.ControlName,
					nameof(Response.Height),
					true);

			outputFields
				.AssertHasOutputField(
					nameof(Response.Weight),
					NumberOutputFieldBinding.ControlName,
					nameof(Response.Weight),
					true,
					0,
					new[] { OutputFieldEventHandlerAttribute.Identifier })
				.HasCustomProperty("help", "this is help text")
				.HasCustomProperty("number", 456);

			outputFields
				.AssertHasOutputField(
					nameof(Response.OtherPeople),
					MetadataBinder.ObjectListOutputControlName,
					nameof(Response.OtherPeople))
				.HasCustomProperty("style", "cool")
				.HasCustomProperty("secret", 321)
				.HasCustomProperty<IList<object>>(
					"documentation",
					t => t.Cast<string>().Count() == 2,
					"Custom property 'documentation' has incorrect value.");

			outputFields
				.AssertHasOutputField(
					nameof(Response.Categories),
					MetadataBinder.ValueListOutputControlName,
					nameof(Response.Categories));

			outputFields
				.AssertHasOutputField(
					nameof(Response.MainPeople),
					"paginated-data",
					nameof(Response.MainPeople))
				.HasCustomProperty("style", "main");

			var ienumerableProperty = outputFields.Single(t => t.Id == nameof(Response.OtherPeople));

			Assert.True(ienumerableProperty.CustomProperties?.ContainsKey("Customizations"));
			
			var columns = ienumerableProperty.CustomProperties?["Columns"] as IList<OutputFieldMetadata> ?? new List<OutputFieldMetadata>();
			
			columns.AssertHasOutputField(nameof(Person.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			columns.AssertHasOutputField(nameof(Person.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2);
			columns.AssertHasOutputField(nameof(Person.Height), NumberOutputFieldBinding.ControlName, nameof(Person.Height), true);
			columns.AssertHasOutputField(nameof(Person.Weight), NumberOutputFieldBinding.ControlName, nameof(Person.Weight), true);
		}

		[Fact]
		public void DuplicateAttemptsToBindSameAssemblyAreIgnored()
		{
			this.binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
			this.binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
		}

		[Fact]
		public void EnumerableOfUnknownTypeIsBoundToTable()
		{
			var outputField = this.binder.BindOutputFields<Response>().Single(t => t.Id == nameof(Response.OtherPeople));

			Assert.Equal(MetadataBinder.ObjectListOutputControlName, outputField.Type);
		}

		[Fact]
		public void EventHandlerCanOnlyBeAppliedToIntendedElements()
		{
			Assert.Throws<BindingException>(() => this.binder.BindInputFields<InvalidRequest>().ToList());
			Assert.Throws<BindingException>(() => this.binder.BindOutputFields<InvalidResponse>().ToList());
		}

		[Fact]
		public void StrictModeIgnoresPropertiesWithoutAttribute()
		{
			var inputs = this.binder
				.BindInputFields(typeof(InputsAndOutputsTogether), strict: true)
				.ToList();

			var outputs = this.binder
				.BindOutputFields(typeof(InputsAndOutputsTogether), strict: true)
				.ToList();

			Assert.Equal(1, inputs.Count);
			Assert.Equal(nameof(InputsAndOutputsTogether.Value), inputs[0].Id);

			Assert.Equal(1, outputs.Count);
			Assert.Equal(nameof(InputsAndOutputsTogether.Label), outputs[0].Id);
		}
	}
}