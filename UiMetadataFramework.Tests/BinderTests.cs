namespace UiMetadataFramework.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Output;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;
	using Xunit;

	public class BinderTests
	{
		public class Response : FormResponse
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			public IList<Person> OtherPeople { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		public class Request
		{
			[Option("Low", "L")]
			[Option("Mid", "M")]
			[Option("High", "H")]
			public DropdownValue<string> Category { get; set; }

			[InputField(Label = "DoB", OrderIndex = 2, Required = true)]
			public DateTime DateOfBirth { get; set; }

			public DropdownValue<DayOfWeek?> Day { get; set; }

			[Option(DayOfWeek.Sunday)]
			[Option(DayOfWeek.Monday)]
			public DropdownValue<DayOfWeek> FirstDayOfWeek { get; set; }

			[InputField(Label = "First name", OrderIndex = 1, Required = true)]
			public string FirstName { get; set; }

			[InputField(Hidden = true)]
			public int? Height { get; set; }

			public bool IsRegistered { get; set; }

			[InputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		public class Person
		{
			[OutputField(Label = "DoB", OrderIndex = 2)]
			public DateTime? DateOfBirth { get; set; }

			[OutputField(Label = "First name", OrderIndex = 1)]
			public string FirstName { get; set; }

			[OutputField(Hidden = true)]
			public int Height { get; set; }

			[OutputField(Hidden = true)]
			public decimal Weight { get; set; }
		}

		[Fact]
		public void CanGetInputFieldsMetadata()
		{
			var binder = new MetadataBinder();
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var inputFields = binder.BindInputFields<Request>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(8, inputFields.Count);
			inputFields.AssertHasInputField(nameof(Request.FirstName), StringInputFieldBinding.ControlName, "First name", orderIndex: 1, required: true);
			inputFields.AssertHasInputField(nameof(Request.DateOfBirth), DateTimeInputFieldBinding.ControlName, "DoB", orderIndex: 2, required: true);
			inputFields.AssertHasInputField(nameof(Request.Height), NumberInputFieldBinding.ControlName, nameof(Request.Height), hidden: true);
			inputFields.AssertHasInputField(nameof(Request.Weight), NumberInputFieldBinding.ControlName, nameof(Request.Weight), hidden: true);
			inputFields.AssertHasInputField(nameof(Request.IsRegistered), BooleanInputFieldBinding.ControlName, nameof(Request.IsRegistered));

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
			var binder = new MetadataBinder();
			binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);

			var outputFields = binder.BindOutputFields<Response>().OrderBy(t => t.OrderIndex).ToList();

			Assert.Equal(5, outputFields.Count);
			outputFields.AssertHasOutputField(nameof(Response.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			outputFields.AssertHasOutputField(nameof(Response.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2);
			outputFields.AssertHasOutputField(nameof(Response.Height), NumberOutputFieldBinding.ControlName, nameof(Response.Height), true);
			outputFields.AssertHasOutputField(nameof(Response.Weight), NumberOutputFieldBinding.ControlName, nameof(Response.Weight), true);
			outputFields.AssertHasOutputField(nameof(Response.OtherPeople), MetadataBinder.EnumerableClientControlName, nameof(Response.OtherPeople));

			var ienumerableProperty = outputFields.Single(t => t.Id == nameof(Response.OtherPeople));

			var columns = ((EnumerableOutputFieldProperties)ienumerableProperty.CustomProperties).Columns;
			columns.AssertHasOutputField(nameof(Person.FirstName), StringOutputFieldBinding.ControlName, "First name", false, 1);
			columns.AssertHasOutputField(nameof(Person.DateOfBirth), DateTimeOutputFieldBinding.ControlName, "DoB", false, 2);
			columns.AssertHasOutputField(nameof(Person.Height), NumberOutputFieldBinding.ControlName, nameof(Response.Height), true);
			columns.AssertHasOutputField(nameof(Person.Weight), NumberOutputFieldBinding.ControlName, nameof(Response.Weight), true);
		}
	}
}