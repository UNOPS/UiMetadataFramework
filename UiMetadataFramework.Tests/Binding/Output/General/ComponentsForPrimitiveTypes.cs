namespace UiMetadataFramework.Tests.Binding.Output.General;

using System.Linq;
using UiMetadataFramework.Basic.Output.Number;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class ComponentsForPrimitiveTypes
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[OutputField(Label = "First name", OrderIndex = 1)]
		public string? FirstName { get; set; }

		[OutputField(Hidden = true)]
		public int Height { get; set; }

		public string? LastName { get; set; }
	}

	[Fact]
	public void CanGetBasicMetadata()
	{
		var outputFields = this.binder
			.BuildOutputFields<Response>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		Assert.Equal(3, outputFields.Count);

		outputFields
			.AssertHasOutputField(
				id: nameof(Response.FirstName),
				type: StringOutputComponentBinding.ControlName,
				label: "First name",
				hidden: false,
				orderIndex: 1);

		outputFields
			.AssertHasOutputField(
				id: nameof(Response.LastName),
				type: StringOutputComponentBinding.ControlName,
				label: "LastName");

		outputFields
			.AssertHasOutputField(
				nameof(Response.Height),
				NumberOutputComponentBinding.ControlName,
				nameof(Response.Height),
				true);
	}
}