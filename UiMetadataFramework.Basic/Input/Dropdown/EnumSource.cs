namespace UiMetadataFramework.Basic.Input.Dropdown;

using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;

/// <summary>
/// Inlines source that retrieves all values of an enum.
/// </summary>
/// <typeparam name="T"></typeparam>
public class EnumSource<T> : IDropdownInlineSource
{
	/// <inheritdoc />
	public IEnumerable<DropdownItem> GetItems()
	{
		return Enum.GetValues(typeof(T))
			.Cast<Enum>()
			.Select(t => new DropdownItem(t.Humanize(), t.ToString()))
			.ToList();
	}
}