// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output.General;

using System.Linq;
using UiMetadataFramework.Basic.Output.FormLink;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Flexbox;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class GenericComponent
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Outputs
	{
		[Flexbox(Style = "fancy")]
		public Flexbox<Item>? Values { get; set; }
	}

	public class Item
	{
		public int Counter { get; set; }
		public FormLink? Link { get; set; }
	}

	[Fact]
	public void ComponentBindingFound()
	{
		var outputField = this.binder.Outputs
			.GetFields(typeof(Outputs))
			.Single(t => t.Id == nameof(Outputs.Values));

		Assert.Equal("flexbox", outputField.Component.Type);

		var fields = (IFieldMetadata[])outputField.Component.ConfigAsDictionary()!["Fields"]!;

		Assert.Equal(2, fields.Length);
	}
}