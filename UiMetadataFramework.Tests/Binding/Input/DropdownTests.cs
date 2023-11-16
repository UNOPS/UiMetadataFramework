namespace UiMetadataFramework.Tests.Binding.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Basic.Inputs.Dropdown;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DropdownTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	private class Request
	{
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

	[Fact]
	public void CanBindToCustomInlineSource()
	{
		var inputFields = this.binder.BindInputFields<Request>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		inputFields
			.AssertHasInputField(nameof(Request.Gender))
			.HasCustomProperty<IEnumerable<DropdownItem>>(
				"Items",
				t => t.Count() == 2,
				"");
	}

	[Fact]
	public void CanBindToEnum()
	{
		var inputFields = this.binder.BindInputFields<Request>()
			.OrderBy(t => t.OrderIndex)
			.ToList();

		inputFields
			.AssertHasInputField(nameof(Request.Day))
			.HasCustomProperty<IList<DropdownItem>>(
				"Items",
				t => t.Count == 7,
				"Dropdown has incorrect number of items.");
	}
}