// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Input;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs.Checkbox;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class CustomInputTypeTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
		[Checkbox(Style = "fancy")]
		public Checkbox? AcceptTerms { get; set; }
	}

	private class RequestWithMissingMandatoryAttribute
	{
		public Checkbox? AcceptTerms { get; set; }
	}

	[Fact]
	public void BindsCustomInputType()
	{
		var inputField = this.binder
			.BindInputFields<Request>()
			.Single(t => t.Id == nameof(Request.AcceptTerms));

		var custom = inputField.Component.GetConfigurationOrException<CheckboxAttribute.Configuration>();

		Assert.Equal("fancy", custom.Style);
	}

	[Fact]
	public void ThrowsExceptionWhenMandatoryAttributeIsMissing()
	{
		Assert.Throws<BindingException>(() => this.binder.BindInputFields<RequestWithMissingMandatoryAttribute>().ToList());
	}
}