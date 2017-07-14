namespace UiMetadataFramework.Basic.Response
{
	using UiMetadataFramework.Core;

	public class MessageResponse : FormResponse
	{
		public MessageResponse() : base("message")
		{
		}

		public string Message { get; set; }
	}
}