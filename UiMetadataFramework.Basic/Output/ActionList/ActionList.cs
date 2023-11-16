namespace UiMetadataFramework.Basic.Output.ActionList
{
	using System.Collections.Generic;
	using System.Linq;
	using UiMetadataFramework.Basic.Output.FormLink;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents a list of actions that user can perform.
	/// </summary>
	[OutputFieldType("action-list")]
	public class ActionList
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionList"/> class.
		/// </summary>
		public ActionList(params FormLink[] actions)
		{
			// We need to convert actions to list, so that items can be added at a later point too.
			// If we don't do ToList, then an exception might be thrown when doing `Actions.Add(a)`,
			// saying "System.NotSupportedException: Collection was of a fixed size."
			this.Actions = actions.ToList();
		}

		/// <summary>
		/// Gets or sets list of actions.
		/// </summary>
		public IList<FormLink> Actions { get; set; }
	}
}