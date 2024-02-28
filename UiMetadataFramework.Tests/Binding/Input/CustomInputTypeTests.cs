// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Input;

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
		var component = this.binder.BuildInputComponent<Request>(t => t.AcceptTerms);

		var config = component.Configuration.As<Dictionary<string, object?>>();

		Assert.Equal("fancy", config["Style"]);
	}

	[Fact]
	public void ThrowsExceptionWhenMandatoryAttributeIsMissing()
	{
		Assert.Throws<BindingException>(() => this.binder.BuildInputFields<RequestWithMissingMandatoryAttribute>().ToList());
	}
}