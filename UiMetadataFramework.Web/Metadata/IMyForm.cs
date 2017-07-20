namespace UiMetadataFramework.Web.Metadata
{
	using global::MediatR;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.MediatR;

	public interface IMyForm<in TRequest, out TResponse> : IForm<TRequest, TResponse, MyFormResponseMetadata>
		where TResponse : FormResponse<MyFormResponseMetadata>
		where TRequest : IRequest<TResponse>
	{
	}

	public interface IMyForm<in TRequest, out TResponse, TResponseMetadata> : IForm<TRequest, TResponse, TResponseMetadata>
		where TResponse : FormResponse<TResponseMetadata>
		where TRequest : IRequest<TResponse>
		where TResponseMetadata : MyFormResponseMetadata
	{
	}
}