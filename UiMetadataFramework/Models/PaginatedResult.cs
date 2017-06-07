namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;
	using UiMetadataFramework.Core.UI.Actions;

	public class PaginatedResult<T> : IHasActions
	{
		[UiProperty(OrderIndex = -1)]
		public IEnumerable<T> Results { get; set; }

		[UiProperty(Hidden = true)]
		public int TotalCount { get; set; }

		[UiProperty(OrderIndex = -2)]
		public ICollection<ActionButton> Actions { get; set; }
	}
}