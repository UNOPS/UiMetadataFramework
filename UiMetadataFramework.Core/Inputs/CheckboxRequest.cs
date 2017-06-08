namespace UiMetadataFramework.Core.Inputs
{
	using System.Collections.Generic;

	public abstract class CheckboxRequest : CheckboxRequest<bool>
	{
	}

	public class CheckboxRequest<TItemValue>
	{
		/// <summary>
		/// Gets value indicating whether request is a query (user searching) or is a request
		/// for specific items.
		/// </summary>
		public bool GetByIds => this.Ids?.Count > 0;

		public List<TItemValue> Ids { get; set; }

		public string Query { get; set; }
	}
}