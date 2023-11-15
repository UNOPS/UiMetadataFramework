namespace UiMetadataFramework.MediatR
{
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Base class for implement a UI Metadata Framework form.
	/// </summary>
	public abstract class Form<TRequest, TResponse, TResponseMetadata>
		: AsyncForm<TRequest, TResponse, TResponseMetadata>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse<TResponseMetadata>
		where TResponseMetadata : FormResponseMetadata
	{
		/// <inheritdoc />
		public override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(this.Handle(request));
		}

		/// <summary>Handles a request</summary>
		/// <param name="request">The request</param>
		/// <returns>Response from the request</returns>
		protected abstract TResponse Handle(TRequest request);
	}

	/// <summary>
	/// Base class for implement a UI Metadata Framework form.
	/// </summary>
	public abstract class Form<TRequest, TResponse>
		: Form<TRequest, TResponse, FormResponseMetadata>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse<FormResponseMetadata>
	{
	}
}