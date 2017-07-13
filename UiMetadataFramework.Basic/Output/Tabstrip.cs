namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents a list of links which should be rendered as tabs.
	/// </summary>
	[OutputFieldType("tabstrip")]
	public class Tabstrip
	{
		/// <summary>
		/// Gets or sets id of the current tab (should correspond to <see cref="FormMetadata.Id"/>).
		/// </summary>
		public string CurrentTab { get; set; }

		/// <summary>
		/// Gets or sets tabs to be rendered.
		/// </summary>
		public IList<Tab> Tabs { get; set; }
	}

	public class Tab : FormLink
	{
		/// <summary>
		/// Arbitrary string representing style of the tab.
		/// </summary>
		public string Style { get; set; }
	}
}