namespace UiMetadataFramework.Tests.Binding.Output.Configuration.DefaultMetadataFactory;

using System.Collections.Generic;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MandatoryConfigurations
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Response
	{
		[Grid]
		public Grid<string>? Grid { get; set; }

		[Grid(Areas = "abc")]
		public Grid<string>? GridWithAreas { get; set; }
	}

	[Fact]
	public void MandatoryAttributeBound()
	{
		var gridWithAreas = this.binder
			.BuildOutputComponent<Response>(t => t.GridWithAreas)
			.Configuration.As<Dictionary<string, object?>>();

		var gridWithoutAreas = this.binder
			.BuildOutputComponent<Response>(t => t.Grid)
			.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal("abc", gridWithAreas["Areas"]);
		Assert.Null(gridWithoutAreas);
	}
}