namespace UiMetadataFramework.Core.Binding
{
	using System;
	using System.Reflection;

	/// <summary>
	/// Decorates field indicating that it will subscribe to a client-side event
	/// and will run a client-function when the event occurs.
	/// </summary>
	public abstract class FieldEventHandlerAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FieldEventHandlerAttribute"/> class.
		/// </summary>
		/// <param name="id">Id of the client function.</param>
		/// <param name="runAt">Event at which to run the client function.</param>
		/// <param name="applicableToInputField">Indicates whether this attribute can be applied to an input field.</param>
		/// <param name="applicableToOutputField">Indicates whether this attribute can be applied to an output field.</param>
		protected FieldEventHandlerAttribute(string id, string runAt, bool applicableToInputField, bool applicableToOutputField)
		{
			this.Id = id;
			this.RunAt = runAt;
			this.ApplicableToInputField = applicableToInputField;
			this.ApplicableToOutputField = applicableToOutputField;
		}

		/// <summary>
		/// Gets a value indicating whether this attribute can be applied to an input field.
		/// </summary>
		/// <remarks>If set to false, then <see cref="MetadataBinder"/> will throw <see cref="BindingException"/> 
		/// in case this attribute is being applied to an input field.</remarks>
		public bool ApplicableToInputField { get; }

		/// <summary>
		/// Gets a value indicating whether this attribute can be applied to an output field.
		/// </summary>
		/// <remarks>If set to false, then <see cref="MetadataBinder"/> will throw <see cref="BindingException"/> 
		/// in case this attribute is being applied to an output field.</remarks>
		public bool ApplicableToOutputField { get; }

		/// <summary>
		/// Gets ID of the client-side function which will run.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Gets event on which the function will run.
		/// </summary>
		/// <remarks><see cref="FormEvents"/> enumerates standard form events, which will be sufficient
		/// for most of the use cases. However each client might have their own custom events.</remarks>
		public string RunAt { get; }

		/// <summary>
		/// Gets custom properties for the function.
		/// </summary>
		/// <param name="property">Property representing the field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>An object with additional metadata describing how to run the function.</returns>
		public virtual object GetCustomProperties(PropertyInfo property, MetadataBinder binder)
		{
			return null;
		}

		/// <summary>
		/// Converts attribute to the metadata.
		/// </summary>
		/// <param name="property">Property representing the field for which to get metadata.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Metadata for the function.</returns>
		public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
		{
			return new EventHandlerMetadata(this.Id, this.RunAt)
			{
				CustomProperties = this.GetCustomProperties(property, binder)
			};
		}
	}
}