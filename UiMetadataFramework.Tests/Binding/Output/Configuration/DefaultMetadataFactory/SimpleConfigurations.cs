namespace UiMetadataFramework.Tests.Binding.Output.Configuration.DefaultMetadataFactory;

using System.Collections.Generic;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Icon;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class SimpleConfigurations
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Response
	{
		[IconBackgroundData(Color = "white", Pattern = null)]
		public Icon? WithNullProperty { get; set; }

		[IconColorData(Color = "blue")]
		public Icon? WithOneConfig { get; set; }

		public Icon? WithoutConfiguration { get; set; }

		[IconColorData(Color = "blue")]
		[IconBackgroundData(Color = "white", Pattern = "stripes")]
		public Icon? WithTwoConfigs { get; set; }
	}

	[Fact]
	public void EmptyConfigurationYieldsNull()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.WithoutConfiguration);

		Assert.Null(field.Configuration);
	}

	[Fact]
	public void NullPropertiesNotAdded()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.WithNullProperty);

		var component = field.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal("white", component["Background"]);
		Assert.Equal(1, component.Count);
	}

	[Fact]
	public void SpecifiedConfigsAdded()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.WithTwoConfigs);

		var component = field.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal(3, component.Count);
		Assert.Equal("blue", component["Color"]);
		Assert.Equal("white", component["Background"]);
		Assert.Equal("stripes", component["Pattern"]);
	}

	[Fact]
	public void UnspecifiedConfigsNotAdded()
	{
		var field = this.binder.BuildOutputComponent<Response>(t => t.WithOneConfig);

		var component = field.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal("blue", component["Color"]);
		Assert.Equal(1, component.Count);
	}
}