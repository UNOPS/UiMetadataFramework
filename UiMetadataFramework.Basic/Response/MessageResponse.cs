namespace UiMetadataFramework.Basic.Response
{
	using UiMetadataFramework.Core;

	/// <summary>
	/// Represents a response which displays a message to the user.
	/// </summary>
	public class MessageResponse : FormResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageResponse"/> class.
		/// </summary>
		public MessageResponse() : base("message")
		{
		}

		/// <summary>
		/// Message to be displayed.
		/// </summary>
		public string Message { get; set; } = null!;
	}
}