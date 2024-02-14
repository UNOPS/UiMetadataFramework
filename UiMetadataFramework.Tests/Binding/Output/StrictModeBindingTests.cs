// ReSharper disable UnusedMember.Local

namespace UiMetadataFramework.Tests.Binding.Output;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class StrictModeBindingTests
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
		var outputs = this.binder
			.BindOutputFields(typeof(InputsAndOutputsTogether), strict: true)
			.ToList();

		Assert.Equal(1, outputs.Count);
		Assert.Equal(nameof(InputsAndOutputsTogether.Label), outputs[0].Id);
	}
}