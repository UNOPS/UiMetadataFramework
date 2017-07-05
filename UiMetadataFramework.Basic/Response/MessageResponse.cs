namespace UiMetadataFramework.Basic.Response
{
	using UiMetadataFramework.Core;

	public class MessageResponse : FormResponse
	{
		public MessageResponse()
		{
			this.ResponseHandler = "message";
		}

		public string Message { get; set; }
	}
}