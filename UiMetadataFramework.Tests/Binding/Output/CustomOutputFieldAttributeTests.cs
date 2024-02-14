namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Custom;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class CustomOutputFieldAttributeTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[CustomOutputField(Hidden = true, Style = "fancy-output")]
		public decimal Weight { get; set; }
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
}