namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class ChecklistInput : Input
	{
		public ChecklistInput(string name, List<ListItem> values) : base(name, "checklist", values)
		{
		}

		public ChecklistInput(string name, string source) : base(name, "checklist", source)
		{
		}

		public static ChecklistInput ForEnum<T>(string name)
		{
			var listItems = typeof(T).GetTypeInfo().GetEnumValues().Cast<T>().Select(t => new ListItem
			{
				Value = t.ToString(),
				Label = t.ToString()
			}).ToList();

			return new ChecklistInput(name, listItems);
		}

		public class ListItem
		{
			public string Label { get; set; }
			public string Value { get; set; }
		}
	}
}