namespace UiMetadataFramework.Web.Metadata
{
	using global::MediatR;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.MediatR;
	
	public abstract class MyForm<TRequest, TResponse> : Form<TRequest, TResponse, MyFormResponseMetadata> 
		where TRequest : IRequest<TResponse> 
		where TResponse : FormResponse<MyFormResponseMetadata>
	{
	}

	public abstract class MyForm<TRequest, TResponse, TResponseMetadata> : Form<TRequest, TResponse, TResponseMetadata> 
		where TRequest : IRequest<TResponse> 
		where TResponse : FormResponse<TResponseMetadata>
		where TResponseMetadata : MyFormResponseMetadata
	{
	}
}