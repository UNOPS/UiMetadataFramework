namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Specifies client-side events of a form.
	/// </summary>
	public static class FormEvents
	{
		/// <summary>
		/// Represents the time when the form has been loaded and fully initialized on the client,
		/// and before any requests to the server are made.
		/// </summary>
		public const string FormLoaded = "form:loaded";

		/// <summary>
		/// Represents the time just before form is posted to the server.
		/// </summary>
		public const string FormPosting = "form:posting";
		
		/// <summary>
		/// Represents the time when client has received the response, 
		/// but just before it has handled it.
		/// </summary>
		public const string ResponseReceived = "form:responseReceived";

		/// <summary>
		/// Represents the time right after client has handled the response.
		/// </summary>
		public const string ResponseHandled = "form:responseHandled";
	}
}