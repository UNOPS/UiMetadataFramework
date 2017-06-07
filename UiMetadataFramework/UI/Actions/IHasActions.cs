namespace UiMetadataFramework.Core.UI.Actions
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Models;

	/// <summary>
	/// Augments any object with actions, which should be rendered as buttons in the UI.
	/// </summary>
	public interface IHasActions
	{
		ICollection<ActionButton> Actions { get; }
	}
}