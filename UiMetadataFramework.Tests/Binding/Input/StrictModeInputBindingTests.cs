namespace UiMetadataFramework.Tests.Binding.Input;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class StrictModeInputBindingTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class InputsAndOutputsTogether
	{
		[OutputField]
		public string? Label { get; set; }

		public string? NotField { get; set; }

		[InputField]
		public string? Value { get; set; }
	}

	[Fact]
	public void StrictModeIgnoresPropertiesWithoutAttribute()
	{
		var inputs = this.binder
			.BuildInputFields(typeof(InputsAndOutputsTogether), strict: true)
			.ToList();

		Assert.Equal(1, inputs.Count);
		Assert.Equal(nameof(InputsAndOutputsTogether.Value), inputs[0].Id);
	}
}