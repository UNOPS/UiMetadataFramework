// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output.Panel;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.ComplexOutput;
using UiMetadataFramework.Tests.Framework.Outputs.Panel;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class PanelTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		public Info? User { get; set; }
	}

	private class Info : Panel
	{
		public int Age { get; set; }
		public string? Name { get; set; }
	}

	[Fact]
	public void CanBindPanel()
	{
		var fields = this.binder.Outputs.GetFields(typeof(Response));

		fields.First().Component.Type.Should().Be(ComplexOutput.Type);
	}
}