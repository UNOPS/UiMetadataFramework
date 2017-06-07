namespace UiMetadataFramework.Core.UI.Inputs
{
	using UiMetadataFramework.Core.Models;

	public class YesNoPicker : DropdownList
	{
		public YesNoPicker(string name) : base(name, new DropdownItem("Yes", "true"), new DropdownItem("No", "false"))
		{
		}

		public YesNoPicker(string name, string yes, string no) : base(name, new DropdownItem(yes, "true"), new DropdownItem(no, "false"))
		{
		}
	}
}