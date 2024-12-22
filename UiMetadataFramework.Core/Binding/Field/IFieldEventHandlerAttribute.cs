namespace UiMetadataFramework.Core.Binding
{
	using System.Reflection;

	/// <summary>
	/// Represents an attribute which can be applied to a field (input or output). This attribute indicates, 
	/// that that the field will subscribe to a client-side event and will run a client-function when the event occurs.
	/// </summary>
	public interface IFieldEventHandlerAttribute : IEventHandlerAttribute
	{
		/// <summary>
		/// Gets a value indicating whether this attribute can be applied to an input field.
		/// </summary>
		/// <remarks>If set to false, then <see cref="MetadataBinder"/> will throw <see cref="BindingException"/> 
		/// in case this attribute is being applied to an input field.</remarks>
		bool ApplicableToInputField { get; }

		/// <summary>
		/// Gets a value indicating whether this attribute can be applied to an output field.
		/// </summary>
		/// <remarks>If set to false, then <see cref="MetadataBinder"/> will throw <see cref="BindingException"/> 
		/// in case this attribute is being applied to an output field.</remarks>
		bool ApplicableToOutputField { get; }

		/// <summary>
		/// Converts attribute to the metadata.
		/// </summary>
		/// <param name="property">Property representing the field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Metadata for the function.</returns>
		EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder);
	}
}