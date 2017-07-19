namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents subset of data from a data store. This subset of data corresponds
	/// to single "page".
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PaginatedData<T>
	{
		/// <summary>
		/// Gets or sets items.
		/// </summary>
		public IEnumerable<T> Results { get; set; }

		/// <summary>
		/// Gets or sets total number of matching items in the data store.
		/// </summary>
		[OutputField(Hidden = true)]
		public int TotalCount { get; set; }
	}
}