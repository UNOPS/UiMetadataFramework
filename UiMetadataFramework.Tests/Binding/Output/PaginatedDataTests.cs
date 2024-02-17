namespace UiMetadataFramework.Tests.Binding.Output;

using System;
using System.Linq;
using UiMetadataFramework.Basic.Output.PaginatedData;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class PaginatedDataTests
{
	public class Response
	{
		[PaginatedData("paginator-for-items")]
		public PaginatedData<Item> Items { get; set; }
	}

	public class Item
	{
		public DateTime DateOfBirth { get; set; }
		public string Name { get; set; }
	}

	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void MetadataBoundCorrectly()
	{
		var metadata = this.binder.BindOutputFields<Response>().Single();

		metadata.HasCustomProperty<PaginatedDataAttribute.Properties>(
			PaginatedDataAttribute.PropertyName,
			t =>
			{
				Assert.Equal("paginator-for-items", t.Paginator);

				Assert.Equal(2, t.Columns?.Count);

				Assert.Collection(
					t.Columns!,
					e => Assert.Equal("DateOfBirth", e.Id),
					e => Assert.Equal("Name", e.Id));

				return true;
			},
			"Paginator is not set correctly.");
	}
}