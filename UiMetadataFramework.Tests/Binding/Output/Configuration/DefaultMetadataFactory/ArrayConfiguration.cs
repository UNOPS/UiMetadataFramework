namespace UiMetadataFramework.Tests.Binding.Output.Configuration.DefaultMetadataFactory;

using System;
using System.Collections.Generic;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Icon;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class ArrayConfiguration
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Response
	{
		[IconStyleData("very")]
		[IconStyleData("nice")]
		public Icon? WithMultipleUsages { get; set; }

		[IconColorData(Color = "blue")]
		public Icon? WithNoUsages { get; set; }

		[IconStyleData("nice")]
		public Icon? WithOneUsage { get; set; }
	}

	public void NoUsagesResultsInNull()
	{
		var component = this.binder.BuildOutputComponent<Response>(t => t.WithNoUsages);

		var config = component.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal(1, config.Count);
		Assert.Equal("blue", config["Color"]);
	}

	[Fact]
	public void AllConfigurationsAddedToList()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.WithMultipleUsages);

		var config = field.Configuration.As<Dictionary<string, object?>>();

		Assert.Collection(
			config["Style"] as IEnumerable<IDictionary<string, object?>?> ?? Array.Empty<IDictionary<string, object?>?>(),
			t => Assert.Equal("very", t!["Style"]),
			t => Assert.Equal("nice", t!["Style"]));
	}

	[Fact]
	public void SingleConfigurationRecordedAsList()
	{
		var component = this.binder.BuildOutputComponent<Response>(t => t.WithOneUsage);

		var config = component.Configuration.As<Dictionary<string, object?>>();

		Assert.Collection(
			config["Style"] as IEnumerable<IDictionary<string, object?>?> ?? Array.Empty<IDictionary<string, object?>?>(),
			t => Assert.Equal("nice", t!["Style"]));
	}
}