namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Basic.Output.Number;
using UiMetadataFramework.Basic.Output.PaginatedData;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class GeneralOutputBindingTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[OutputField(Label = "First name", OrderIndex = 1)]
		public string? FirstName { get; set; }

		[OutputField(Hidden = true)]
		public int Height { get; set; }

		[PaginatedData("MainPeoplePaginator")]
		public PaginatedData<Item>? Items { get; set; }

		public string? LastName { get; set; }
	}

	private sealed class Item
	{
	}

	[Fact]
	public void CanGetBasicMetadata()
	{
		var outputFields = this.binder
			.BindOutputFields<Response>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		Assert.Equal(4, outputFields.Count);

		outputFields
			.AssertHasOutputField(
				id: nameof(Response.FirstName),
				type: StringOutputFieldBinding.ControlName,
				label: "First name",
				hidden: false,
				orderIndex: 1);

		outputFields
			.AssertHasOutputField(
				nameof(Response.Height),
				NumberOutputFieldBinding.ControlName,
				nameof(Response.Height),
				true);

		outputFields
			.AssertHasOutputField(
				nameof(Response.Items),
				"paginated-data",
				nameof(Response.Items));
	}

	[Fact]
	public void OutputFieldAttributeIsOptional()
	{
		var outputFields = this.binder
			.BindOutputFields<Response>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		outputFields.AssertHasOutputField(nameof(Response.LastName));
	}
}