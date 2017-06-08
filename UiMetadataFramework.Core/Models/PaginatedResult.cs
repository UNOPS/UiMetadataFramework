namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;

	public class PaginatedResult<T> : IHasActions
	{
		[UiProperty(OrderIndex = -1)]
		public IEnumerable<T> Results { get; set; }

		public int TotalCount { get; set; }

		[UiProperty(OrderIndex = -2)]
		public ICollection<ActionButton> Actions { get; set; }
	}
}