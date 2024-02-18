// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output.General;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Flexbox;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class GenericComponent
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[Flexbox(Style = "fancy")]
		public Flexbox<string>? Values { get; set; }
	}

	[Fact]
	public void ComponentBindingFound()
	{
		var outputField = this.binder
			.BindOutputFields<Response>()
			.Single(t => t.Id == nameof(Response.Values));

		Assert.Equal("flexbox", outputField.Type);
	}
}