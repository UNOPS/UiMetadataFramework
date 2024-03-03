namespace UiMetadataFramework.Tests.Binding.Output;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Inputs.Text;
using UiMetadataFramework.Basic.Output.DateTime;
using UiMetadataFramework.Basic.Output.FormLink;
using UiMetadataFramework.Basic.Output.Number;
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

	[Theory]
	[InlineData(nameof(Response.Links), "formlink")]
	[InlineData(nameof(Response.Categories), StringInputComponentBinding.ControlName)]
	[InlineData(nameof(Response.Numbers), NumberOutputComponentBinding.ControlName)]
	public void EnumerableOfComponentHasOneColumn(string property, string itemType)
	{
		var outputField = this.binder.Outputs.GetFields(typeof(Response))
			.Single(t => t.Id == property);

		var config = outputField.Component.ConfigAsDictionary()!;

		Assert.Equal("table", outputField.Component.Type);

		var columns = config["Columns"].As<List<OutputFieldMetadata>>();

		Assert.Equal(1, columns.Count);
		Assert.Equal(itemType, columns.Single().Component.Type);
	}

	[Fact]
	public void EnumerableOfNonComponentHasMultipleColumns()
	{
		var outputFieldMetadatas = this.binder.Outputs.GetFields(typeof(Response))
			.ToList();

		var outputField = outputFieldMetadatas
			.Single(t => t.Id == nameof(Response.RandomObjects));

		var config = outputField.Component.ConfigAsDictionary()!;

		Assert.Equal("table", outputField.Component.Type);

		var columns = config["Columns"].As<List<OutputFieldMetadata>>();

		columns.AssertHasOutputField(
			nameof(Person.FirstName),
			StringOutputComponentBinding.ControlName,
			"First name",
			false,
			1);

		columns.AssertHasOutputField(
			nameof(Person.DateOfBirth),
			DateTimeOutputComponentBinding.ControlName,
			"DoB",
			false,
			2);

		columns.AssertHasOutputField(
			nameof(Person.Height),
			NumberOutputComponentBinding.ControlName,
			nameof(Person.Height),
			true);

		columns.AssertHasOutputField(
			nameof(Person.Weight),
			NumberOutputComponentBinding.ControlName,
			nameof(Person.Weight),
			true);
	}
}