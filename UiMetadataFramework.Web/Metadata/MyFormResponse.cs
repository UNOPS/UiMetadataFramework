namespace UiMetadataFramework.Web.Metadata
{
	using global::MediatR;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.MediatR;

	public class MyFormResponse : FormResponse<MyFormResponseMetadata>
	{
	}

	public interface IMyForm<in TRequest, out TResponse> : IForm<TRequest, TResponse, MyFormResponseMetadata>
		where TResponse : FormResponse<MyFormResponseMetadata>
		where TRequest : IRequest<TResponse>
	{
	}
}