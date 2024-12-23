namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Alert;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MyOutputComponentTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[OutputField(OrderIndex = 10)]
		public Alert? Custom { get; set; }

		[MyOutputField]
		public Alert? Default { get; set; }
	}

	[Fact]
	public void CanBindDerivedOutputComponentAttribute()
	{
		var fields = this.binder.Outputs.GetFields(typeof(Response)).ToList();

		fields.First(t => t.Id == nameof(Response.Custom)).OrderIndex.Should().Be(10);
		fields.First(t => t.Id == nameof(Response.Default)).OrderIndex.Should().Be(-10);
	}
}