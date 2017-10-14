namespace UiMetadataFramework.Web.Metadata.Record
{
	using global::MediatR;
	using UiMetadataFramework.Basic.EventHandlers;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Core.Binding;

	public class RecordRequest<T> : IRequest<T>
		where T : RecordResponse
	{
		[InputField(Hidden = true)]
		[BindToOutput(nameof(RecordResponse.NextOperation))]
		public DropdownValue<RecordRequestOperation?> Operation { get; set; }
	}
}