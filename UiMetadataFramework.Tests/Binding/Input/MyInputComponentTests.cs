namespace UiMetadataFramework.Tests.Binding.Input;

using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs;
using UiMetadataFramework.Tests.Framework.Inputs.Email;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MyInputComponentTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
		[MyInputField(Label = "Custom")]
		public Email? Custom { get; set; }

		[MyInputField]
		public Email? Default { get; set; }
	}

	[Fact]
	public void AdditionalMetadataWorks()
	{
		var fields = this.binder.Inputs.GetFields(typeof(Request)).ToList();

		fields.First(t => t.Id == nameof(Request.Custom)).Label.Should().Be("Custom");
		fields.First(t => t.Id == nameof(Request.Default)).Label.Should().Be("Email address");
	}
}