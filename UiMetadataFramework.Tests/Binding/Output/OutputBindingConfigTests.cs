namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Basic.Output.PaginatedData;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class OutputBindingConfigTests
{
	public class Outputs
	{
		[MyOutputField]
		[PaginatedData("paginator")]
		public PaginatedData<TableTests.Person>? People { get; set; }
	}

	[Fact]
	public void NoDefaultLabelWorks()
	{
		var binder = MetadataBinderFactory.CreateMetadataBinder();
		var field = binder.Outputs.GetFields(typeof(Outputs)).Single();
		field.Label.Should().Be("");
	}
}