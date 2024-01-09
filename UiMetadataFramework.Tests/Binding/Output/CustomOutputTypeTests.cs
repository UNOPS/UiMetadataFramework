// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Flexbox;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class CustomOutputTypeTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class ResponseWithMandatoryAttribute
	{
		[Flexbox]
		public Flexbox<string>? Values { get; set; }
	}

	private class ResponseWithMissingMandatoryAttribute
	{
		public Flexbox<string>? Values { get; set; }
	}

	[Fact]
	public void BindsCustomOutputType()
	{
		var outputField = this.binder
			.BindOutputFields<ResponseWithMandatoryAttribute>()
			.Single(t => t.Id == nameof(ResponseWithMandatoryAttribute.Values));

		var custom = outputField as FlexboxAttribute.Metadata;

		Assert.NotNull(custom);
	}

	[Fact]
	public void ThrowsExceptionWhenMandatoryAttributeIsMissing()
	{
		Assert.Throws<BindingException>(() => this.binder.BindOutputFields<ResponseWithMissingMandatoryAttribute>().ToList());
	}
}