namespace UiMetadataFramework.Core
{
	public abstract class RecordRequest<TResponse> : IRequest<TResponse>
	{
		public object EntityId { get; set; }
		public RecordOperation Operation { get; set; }
	}
}