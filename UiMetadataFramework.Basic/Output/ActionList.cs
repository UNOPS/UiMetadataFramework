namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	[OutputFieldType("action-list")]
	public class ActionList
	{
		public ActionList(params FormLink[] actions)
		{
			this.Actions = actions;
		}

		public IList<FormLink> Actions { get; set; }
	}
}