namespace UiMetadataFramework.MediatR
{
	using System.Linq;
	using global::MediatR;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public static class BinderExtensions
	{
		public static FormMetadata BindForm<TForm, TRequest, TResponse>(this MetadataBinder binder, string label, bool postOnLoad = false)
			where TForm : IForm<TRequest, TResponse> 
			where TResponse : FormResponse 
			where TRequest : IRequest<TResponse>
		{
			return new FormMetadata
			{
				Id = typeof(TForm).FullName,
				Label = label,
				OutputFields = binder.BindOutputFields<TResponse>().ToList(),
				InputFields = binder.BindInputFields<TRequest>().ToList(),
				PostOnLoad = postOnLoad 
			};
		}
	}
}