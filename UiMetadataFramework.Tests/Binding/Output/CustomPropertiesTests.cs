﻿namespace UiMetadataFramework.Tests.Binding.Output;

using System;
using System.Linq;
using UiMetadataFramework.Basic.Output.DateTime;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class CustomPropertiesTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Response
	{
		[IntProperty("secret", 123)]
		[StringProperty("style", "beatiful")]
		[OutputField(Label = "DoB", OrderIndex = 2)]
		public DateTime DateOfBirth { get; set; }
	}

	[Fact]
	public void CustomPropertiesAreBound()
	{
		var outputFields = this.binder.Outputs
			.GetFields(typeof(Response))
			.OrderBy(t => t.OrderIndex)
			.ToList();

		outputFields
			.AssertHasOutputField(
				nameof(Response.DateOfBirth),
				DateTimeOutputComponentBinding.ControlName,
				"DoB",
				false,
				2)
			.HasCustomProperty("style", "beatiful")
			.HasCustomProperty("secret", 123);
	}
}