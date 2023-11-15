namespace UiMetadataFramework.MediatR
{
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Base class for implement a UI Metadata Framework form.
	/// </summary>
	public abstract class AsyncForm<TRequest, TResponse, TResponseMetadata>
		: IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse<TResponseMetadata>
		where TResponseMetadata : FormResponseMetadata
	{
		/// <inheritdoc />
		public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
	}

	/// <summary>
	/// Base class for implement a UI Metadata Framework form.
	/// </summary>
	public abstract class AsyncForm<TRequest, TResponse>
		: AsyncForm<TRequest, TResponse, FormResponseMetadata>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse<FormResponseMetadata>
	{
		/// <inheritdoc />
		public abstract override Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
	}
}