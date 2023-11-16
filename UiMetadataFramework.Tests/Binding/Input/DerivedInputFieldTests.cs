namespace UiMetadataFramework.Tests.Binding.Input;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs.Custom;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DerivedInputFieldTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
		[CustomInputField(Style = "fancy")]
		public bool IsRegistered { get; set; }
	}

	[Fact]
	public void CanBindDerivedInputFieldAttribute()
	{
		var inputField = this.binder
			.BindInputFields<Request>()
			.Single(t => t.Id == nameof(Request.IsRegistered));

		var custom = inputField as CustomInputFieldAttribute.Metadata;

		Assert.NotNull(custom);
		Assert.Equal("fancy", custom!.Style);
	}
}