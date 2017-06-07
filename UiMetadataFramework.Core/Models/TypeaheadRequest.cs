namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;

	public abstract class TypeaheadRequest : TypeaheadRequest<long>
	{
	}

	public class TypeaheadRequest<TItemValue> : TypeaheadRequest<TypeaheadResponse<TItemValue>, TItemValue>
	{
	}

	public class TypeaheadRequest<TResponse, TItemValue> : IRequest<TResponse>
		where TResponse : TypeaheadResponse<TItemValue>
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