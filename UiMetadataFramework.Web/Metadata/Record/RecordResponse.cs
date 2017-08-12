namespace UiMetadataFramework.Web.Metadata.Record
{
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Core.Binding;

	public class RecordResponse : MyFormResponse
	{
		[NotField]
		public DropdownValue<RecordRequestOperation?> NextOperation { get; set; } = new DropdownValue<RecordRequestOperation?>(RecordRequestOperation.Post);
	}
}