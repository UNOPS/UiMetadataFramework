namespace UiMetadataFramework.Tests.Binding.Output.Configuration;

using System;
using System.Linq;
using UiMetadataFramework.Basic.Output.PaginatedData;
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
		[PaginatedData("paginator-for-items")]
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
		var field = this.binder.BindOutputFields<Response>().Single();

		var component = field.GetComponentConfigurationOrException<PaginatedDataAttribute.Properties>();

		Assert.Equal("paginator-for-items", component.Paginator);

		Assert.Equal(2, component.Columns?.Count);

		Assert.Collection(
			component.Columns!,
			e => Assert.Equal("DateOfBirth", e.Id),
			e => Assert.Equal("Name", e.Id));
	}
}