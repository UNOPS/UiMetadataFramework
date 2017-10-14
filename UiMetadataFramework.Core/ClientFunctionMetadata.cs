namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Represents a client-side function.
	/// </summary>
	public class ClientFunctionMetadata : IClientFunctionMetadata
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ClientFunctionMetadata"/> class.
		/// </summary>
		/// <param name="id"></param>
		public ClientFunctionMetadata(string id)
		{
			this.Id = id;
		}

		/// <inheritdoc />
		public object CustomProperties { get; protected set; }

		/// <inheritdoc />
		public string Id { get; }

		/// <summary>
		/// Create <see cref="EventHandlerMetadata"/> which binds this function to a specific client-side event.
		/// </summary>
		/// <param name="runat">Event at which the function will run.</param>
		/// <returns>Instance of <see cref="EventHandlerMetadata"/>.</returns>
		public EventHandlerMetadata AsEventHandlerMetadata(string runat)
		{
			return new EventHandlerMetadata(this.Id, runat)
			{
				CustomProperties = this.CustomProperties
			};
		}
	}
}