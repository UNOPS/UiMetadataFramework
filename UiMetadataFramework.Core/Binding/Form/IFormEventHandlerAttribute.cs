namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Represents an attribute which can be applied to a form. This attribute indicates, 
	/// that that the form will subscribe to a client-side event and will run a client-function when the event occurs.
	/// </summary>
	public interface IFormEventHandlerAttribute : IEventHandlerAttribute
	{
		/// <summary>
		/// Converts attribute to the metadata.
		/// </summary>
		/// <param name="formType">Property representing the form to which this attribute is applied.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Metadata for the function.</returns>
		EventHandlerMetadata ToMetadata(Type formType, MetadataBinder binder);
	}
}