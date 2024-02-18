// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output.BindingModes;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class StrictVsDefaultMode
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
	public void DefaultModeTakesAllPublicProperties()
	{
		var outputFields = this.binder
			.BindOutputFields<InputsAndOutputsTogether>(strict: false)
			.ToList();

		outputFields.AssertHasOutputField(nameof(InputsAndOutputsTogether.Label));
		outputFields.AssertHasOutputField(nameof(InputsAndOutputsTogether.Label));
		outputFields.AssertHasOutputField(nameof(InputsAndOutputsTogether.Label));
	}

	[Fact]
	public void StrictModeTakesPropertiesWithOutputFieldAttributeOnly()
	{
		var outputs = this.binder
			.BindOutputFields<InputsAndOutputsTogether>(strict: true)
			.ToList();

		Assert.Equal(1, outputs.Count);
		Assert.Equal(nameof(InputsAndOutputsTogether.Label), outputs[0].Id);
	}
}