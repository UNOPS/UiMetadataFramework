namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Models;

	public class DropdownList : Input
	{
		private const string Typename = "dropdownlist";

		public DropdownList(string name, string form, params FormParameter[] parameters) : base(name, Typename, parameters)
		{
			this.Form = form;
		}

		public DropdownList(string name, IEnumerable<string> list) : base(name, Typename, list.Select(t => new DropdownItem(t, t)))
		{
		}

		public DropdownList(string name, params DropdownItem[] items) : base(name, Typename, items)
		{
		}

		public string Form { get; set; }

		/// <summary>
		/// Sets if list can have All item
		/// </summary>
		public bool ShowAll { get; set; }
	}
}