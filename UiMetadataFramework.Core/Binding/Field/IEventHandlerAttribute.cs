namespace UiMetadataFramework.Core.Binding
{
	/// <summary>
	/// Represents an attribute indicating that the element it applies to will subscribe to a
	/// client-side event and will run a client-side function when that event occurs.
	/// </summary>
	public interface IEventHandlerAttribute
	{
		/// <summary>
		/// Gets ID of the client-side function which will run.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets event on which the function will run.
		/// </summary>
		/// <remarks><see cref="FormEvents"/> enumerates standard form events, which will be sufficient
		/// for most of the use cases. However each client might have their own custom events.</remarks>
		string RunAt { get; }
	}
}