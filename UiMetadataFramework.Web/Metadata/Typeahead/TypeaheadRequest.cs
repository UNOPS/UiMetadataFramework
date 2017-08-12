namespace UiMetadataFramework.Web.Metadata.Typeahead
{
	using global::MediatR;

	public class TypeaheadRequest<T> : IRequest<TypeaheadResponse<T>>
	{
		public const int ItemsPerRequest = 10;

		public bool GetByIds => this.Ids?.Items?.Count > 0;
		public ValueList<T> Ids { get; set; }
		public string Query { get; set; }
	}
}