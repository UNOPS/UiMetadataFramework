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
		var field = this.binder.BuildInputComponent<Request>(t => t.Countries);

		var component = field.Configuration.As<IDictionary<string, object?>>();

		Assert.False(component.ContainsKey("Items"));

		var parameters = component["Parameters"] as List<RemoteSourceArgument>;

		Assert.Equal("A", parameters?.Single().Parameter);
		Assert.Equal("B", parameters?.Single().Source);
		Assert.Equal("C", parameters?.Single().SourceType);
	}

	[Fact]
	public void CanBindToCustomInlineSource()
	{
		var field = this.binder.BuildInputComponent<Request>(t => t.Gender);

		var component = field.Configuration.As<IDictionary<string, object?>>();

		var items = component["Items"] as List<DropdownItem>;

		Assert.Equal(2, items?.Count);
	}

	[Fact]
	public void CanBindToEnum()
	{
		var field = this.binder.BuildInputComponent<Request>(t => t.Day);

		var component = field.Configuration.As<IDictionary<string, object?>>();

		var items = component["Items"] as List<DropdownItem>;

		Assert.Equal(7, items?.Count);
	}
}