namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;

	public abstract class CheckboxRequest : CheckboxRequest<bool>
	{
	}

	public class CheckboxRequest<TItemValue> : CheckboxRequest<CheckboxResponse<TItemValue>, TItemValue>
	{
	}

	public class CheckboxRequest<TResponse, TItemValue> : IRequest<TResponse>
		where TResponse : CheckboxResponse<TItemValue>
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