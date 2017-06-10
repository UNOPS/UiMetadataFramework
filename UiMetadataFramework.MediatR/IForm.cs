namespace UiMetadataFramework.MediatR
{
	using global::MediatR;
	using UiMetadataFramework.Core;

	public interface IForm<in TRequest, out TResponse> :
		IRequestHandler<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : FormResponse
	{
	}
}