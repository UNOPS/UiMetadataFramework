namespace UiMetadataFramework.Web.Metadata
{
	using global::MediatR;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Core.Binding;

	public class RecordRequest<T> : IRequest<T>
	{
		[InputField(Hidden = true, Required = true)]
		public DropdownValue<RecordRequestOperation> Operation { get; set; }
	}

	public enum RecordRequestOperation
	{
		Get,
		Post
	}
}