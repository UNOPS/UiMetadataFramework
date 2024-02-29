namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System;
using System.Collections.Generic;
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
		[Paginated("paginator-for-items")]
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
		var config = this.binder
			.BuildOutputComponent<Response>(t => t.Items)
			.ConfigAsDictionary()!;

		Assert.Equal("paginator-for-items", config["Paginator"]);

		var columns = config["Columns"].As<IList<OutputFieldMetadata>>();

		Assert.Equal(2, columns.Count);

		Assert.Collection(
			columns,
			e => Assert.Equal("DateOfBirth", e.Id),
			e => Assert.Equal("Name", e.Id));
	}
}