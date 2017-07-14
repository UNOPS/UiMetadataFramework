namespace UiMetadataFramework.MediatR
{
	using global::MediatR;
	using UiMetadataFramework.Core;

	public interface IForm<in TRequest, out TResponse, TResponseMetadata> :
		IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse<TResponseMetadata>
		where TResponseMetadata : FormResponseMetadata
	{
	}

	public interface IForm<in TRequest, out TResponse> : IForm<TRequest, TResponse, FormResponseMetadata>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse
	{
	}
}