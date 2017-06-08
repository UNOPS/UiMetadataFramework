namespace UiMetadataFramework.Core
{
	public abstract class RecordRequest
	{
		public object EntityId { get; set; }
		public RecordOperation Operation { get; set; }
	}
}