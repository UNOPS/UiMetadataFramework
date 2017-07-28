namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Core.Binding;

	[OutputFieldType("action-list")]
	public class ActionList
	{
		public ActionList(params FormLink[] actions)
		{
			// We need to convert actions to list, so that items can be added at a later point too.
			// If we don't do ToList, then an exception might be thrown when doing `Actions.Add(a)`,
			// saying "System.NotSupportedException: Collection was of a fixed size."
			this.Actions = actions.ToList();
		}

		public IList<FormLink> Actions { get; set; }
	}
}