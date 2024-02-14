namespace UiMetadataFramework.Tests.Binding.Output;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Basic.Inputs.Text;
using UiMetadataFramework.Basic.Output.DateTime;
using UiMetadataFramework.Basic.Output.FormLink;
using UiMetadataFramework.Basic.Output.Number;
using UiMetadataFramework.Basic.Output.Table;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class TableTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		public IList<string>? Categories { get; set; }
		public IList<FormLink>? Links { get; set; }
		public int[]? Numbers { get; set; }
		public IList<Person>? RandomObjects { get; set; }
	}

	[Theory]
	[InlineData(nameof(Response.Links), "formlink")]
	[InlineData(nameof(Response.Categories), StringInputFieldBinding.ControlName)]
	[InlineData(nameof(Response.Numbers), NumberOutputFieldBinding.ControlName)]
	public void EnumerableOfKnownOutputTypeIndicatesType(string property, string itemType)
	{
		var outputField = this.binder
			.BindOutputFields<Response>()
			.Single(t => t.Id == property);

		outputField.HasCustomProperty<IEnumerable<OutputFieldMetadata>>("Columns", t => t.Single().Type == itemType);
	}

	[Theory]
	[InlineData(nameof(Response.Links))]
	[InlineData(nameof(Response.Categories))]
	public void EnumerableOfKnownOutputTypeIsBoundToList(string property)
	{
		var outputField = this.binder
			.BindOutputFields<Response>()
			.Single(t => t.Id == property);

		Assert.Equal(TableOutputFieldBinding.ObjectListOutputControlName, outputField.Type);
	}

	public class Person
	{
		[OutputField(Label = "DoB", OrderIndex = 2)]
		public DateTime? DateOfBirth { get; set; }

		[OutputField(Label = "First name", OrderIndex = 1)]
		public string? FirstName { get; set; }

		[OutputField(Hidden = true)]
		public int Height { get; set; }

		[OutputField(Hidden = true)]
		public decimal Weight { get; set; }
	}

	[Fact]
	public void EnumerableOfUnknownTypeIsBoundToTable()
	{
		var outputField = this.binder
			.BindOutputFields<Response>()
			.Single(t => t.Id == nameof(Response.RandomObjects));

		Assert.Equal(TableOutputFieldBinding.ObjectListOutputControlName, outputField.Type);

		var columns =
			outputField.CustomProperties?["Columns"] as IList<OutputFieldMetadata> ??
			new List<OutputFieldMetadata>();

		columns.AssertHasOutputField(
			nameof(Person.FirstName),
			StringOutputFieldBinding.ControlName,
			"First name",
			false,
			1);

		columns.AssertHasOutputField(
			nameof(Person.DateOfBirth),
			DateTimeOutputFieldBinding.ControlName,
			"DoB",
			false,
			2);

		columns.AssertHasOutputField(
			nameof(Person.Height),
			NumberOutputFieldBinding.ControlName,
			nameof(Person.Height),
			true);
		columns.AssertHasOutputField(
			nameof(Person.Weight),
			NumberOutputFieldBinding.ControlName,
			nameof(Person.Weight),
			true);
	}
}