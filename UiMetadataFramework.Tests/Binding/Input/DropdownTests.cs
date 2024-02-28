namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UiMetadataFramework.Basic.Inputs.Dropdown;
using UiMetadataFramework.Basic.Inputs.Typeahead;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DropdownTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
		[Dropdown(typeof(CountryRemoteSource))]
		[RemoteSourceArgumentData("A", "B", "C")]
		public DropdownValue<string>? Countries { get; set; }

		[Dropdown(typeof(EnumSource<DayOfWeek>))]
		public DropdownValue<DayOfWeek>? Day { get; set; }

		[Dropdown(typeof(GenderInlineSource))]
		public DropdownValue<int>? Gender { get; set; }
	}

	private class GenderInlineSource : IDropdownInlineSource
	{
		public IEnumerable<DropdownItem> GetItems()
		{
			return new[]
			{
				new DropdownItem("Female", "female"),
				new DropdownItem("Male", "male")
			};
		}
	}

	[Form]
	private class CountryRemoteSource : IDropdownRemoteSource;

	[Fact]
	public void CanBindRemoteSource()
	{
		var field = this.binder.BuildInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Countries));

		var component = field.Component.Configuration.As<DropdownAttribute.Configuration>();

		Assert.Null(component.Items);
		Assert.Equal("A", component.Parameters?.Single().Parameter);
		Assert.Equal("B", component.Parameters?.Single().Source);
		Assert.Equal("C", component.Parameters?.Single().SourceType);
	}

	[Fact]
	public void CanBindToCustomInlineSource()
	{
		var field = this.binder.BuildInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Gender));

		var component = field.Component.Configuration.As<DropdownAttribute.Configuration>();

		Assert.Equal(2, component.Items?.Count);
	}

	[Fact]
	public void CanBindToEnum()
	{
		var inputFields = this.binder.BuildInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Day));

		var component = inputFields.Component.Configuration.As<DropdownAttribute.Configuration>();

		Assert.Equal(7, component.Items?.Count);
	}
}