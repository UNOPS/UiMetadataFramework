namespace UiMetadataFramework.Web.Metadata.Typeahead
{
	using global::MediatR;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.MediatR;

	public interface ITypeaheadRemoteSource<in TRequest, TKey> : IForm<TRequest, TypeaheadResponse<TKey>>, ITypeaheadRemoteSource
		where TRequest : IRequest<TypeaheadResponse<TKey>>
	{
	}
}