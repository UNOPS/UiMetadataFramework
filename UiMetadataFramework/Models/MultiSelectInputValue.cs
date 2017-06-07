namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;

	public class MultiSelectInputValue<T>
	{
		public IEnumerable<T> Items { get; set; }
		public IEnumerable<string> NewItems { get; set; }
	}
}