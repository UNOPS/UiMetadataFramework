namespace UiMetadataFramework.Tests.Binding.Output;

using FluentAssertions;
using UiMetadataFramework.Tests.Framework.Outputs.Flexbox;
using UiMetadataFramework.Tests.Framework.Outputs.ObjectList;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class RecursiveFieldTest
{
	[Flexbox]
	public class Node : Flexbox
	{
		[ObjectList]
		public ObjectList<Node>? Children { get; set; }
		public string? Name { get; set; }
	}

	[Fact]
	public void CanBindRecursiveField()
	{
		var binder = MetadataBinderFactory.CreateMetadataBinder();
		var field = binder.Outputs.BuildComponent(typeof(Node));

		field.Should().NotBeNull();
	}
}