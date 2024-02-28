namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Output.PaginatedData;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

/// <summary>
/// Makes sure configuration is built for generic components. 
/// </summary>
public class GenericComponent
{
	public class Response
	{
		[PaginatedDataAttribute("paginator-for-items")]
		public PaginatedData<Item>? Items { get; set; }
	}

	public class Item
	{
		public DateTime DateOfBirth { get; set; }
		public string? Name { get; set; }
	}

	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void MetadataBoundCorrectly()
	{
		var field = this.binder.BuildOutputFields<Response>().Single();

		var component = field.Component.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal("paginator-for-items", component["Paginator"]);

		var columns = component["Columns"].As<IList<OutputFieldMetadata>>();

		Assert.Equal(2, columns.Count);

		Assert.Collection(
			columns,
			e => Assert.Equal("DateOfBirth", e.Id),
			e => Assert.Equal("Name", e.Id));
	}
}