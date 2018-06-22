namespace UiMetadataFramework.Web.Metadata.Typeahead
{
	using global::MediatR;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.MediatR;

	public abstract class TypeaheadRemoteSource<TRequest, TKey> : Form<TRequest, TypeaheadResponse<TKey>>, ITypeaheadRemoteSource
		where TRequest : IRequest<TypeaheadResponse<TKey>>
	{
	}
}