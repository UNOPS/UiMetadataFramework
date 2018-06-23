namespace UiMetadataFramework.Basic.Input.Dropdown
{
    using System.Collections.Generic;

    public interface IDropdownInlineSource
    {
        IEnumerable<DropdownItem> GetItems();
    }
}