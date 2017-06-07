namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;

	[UiModel(Name = "tabstrip")]
	public class Tabstrip
	{
		[UiProperty(OrderIndex = 0)]
		public List<ActionButton> Actions { get; set; } = new List<ActionButton>();

		public string ActiveTabId { get; set; }

		[UiProperty(OrderIndex = 1)]
		public ICollection<Tab> Tabs { get; set; } = new List<Tab>();
	}
}