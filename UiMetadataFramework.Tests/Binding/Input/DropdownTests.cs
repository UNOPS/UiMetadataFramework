namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Collections.Generic;
using System.Linq;
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
		[DropdownInputField(typeof(CountryRemoteSource))]
		[RemoteSourceArgument("A", "B", "C")]
		public DropdownValue<string>? Countries { get; set; }

		[DropdownInputField(typeof(EnumSource<DayOfWeek>))]
		public DropdownValue<DayOfWeek>? Day { get; set; }

		[DropdownInputField(typeof(GenderInlineSource))]
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
		var field = this.binder.BindInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Countries));

		var component = field.GetComponentConfigurationOrException<DropdownInputFieldAttribute.Configuration>();

		Assert.Null(component.Items);
		Assert.Equal("A", component.Parameters?.Single().Parameter);
		Assert.Equal("B", component.Parameters?.Single().Source);
		Assert.Equal("C", component.Parameters?.Single().SourceType);
	}

	[Fact]
	public void CanBindToCustomInlineSource()
	{
		var field = this.binder.BindInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Gender));

		var component = field.GetComponentConfigurationOrException<DropdownInputFieldAttribute.Configuration>();

		Assert.Equal(2, component.Items?.Count);
	}

	[Fact]
	public void CanBindToEnum()
	{
		var inputFields = this.binder.BindInputFields<Request>()
			.Single(t => t.Id == nameof(Request.Day));

		var component = inputFields.GetComponentConfigurationOrException<DropdownInputFieldAttribute.Configuration>();

		Assert.Equal(7, component.Items?.Count);
	}
}