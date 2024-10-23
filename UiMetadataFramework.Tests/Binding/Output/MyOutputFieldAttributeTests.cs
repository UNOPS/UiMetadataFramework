namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MyOutputFieldAttributeTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[MyOutputField(Hidden = true, Style = "fancy-output")]
		public decimal Weight { get; set; }
	}

	[Fact]
	public void CanBindDerivedOutputFieldAttribute()
	{
		var outputField = this.binder.Outputs
			.GetFields(typeof(Response))
			.Single(t => t.Id == nameof(Response.Weight));

		var custom = outputField as MyOutputFieldAttribute.Metadata;

		Assert.NotNull(custom);
		Assert.Equal("fancy-output", custom!.Style);
	}
}