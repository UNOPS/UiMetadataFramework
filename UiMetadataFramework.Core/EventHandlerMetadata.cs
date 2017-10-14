namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents a client-side function which will run when a specific client-side event occurs.
	/// </summary>
	public class EventHandlerMetadata : ClientFunctionMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EventHandlerMetadata"/> class.
		/// </summary>
		/// <param name="id">Id of the client-side function.</param>
		/// <param name="runAt">Event at which the function should run.</param>
		public EventHandlerMetadata(string id, string runAt) : base(id)
		{
			this.RunAt = runAt;
		}
		
		/// <summary>Gets or sets event at which the function will run.</summary>
		/// <remarks><see cref="FormEvents"/> enumerates standard form events, which will be sufficient
		/// for most of the use cases. However each client might have their own custom events.</remarks>
		public string RunAt { get; }
	}
}