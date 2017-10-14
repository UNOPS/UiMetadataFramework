namespace UiMetadataFramework.MediatR
{
	using System;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Decorates a form to indicate that a client-side function should be 
	/// run when a specific client-side event occurs.
	/// </summary>
	public class FormEventHandlerAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FormEventHandlerAttribute"/> class.
		/// </summary>
		public FormEventHandlerAttribute(string id, string runAt)
		{
			this.Id = id;
			this.RunAt = runAt;
		}

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
		/// <param name="formType">Type to which this attribute is applied.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>An object with additional metadata describing how to run the function.</returns>
		public virtual object GetCustomProperties(Type formType, MetadataBinder binder)
		{
			return null;
		}

		/// <summary>
		/// Converts attribute to the metadata.
		/// </summary>
		/// <param name="formType">Type to which this attribute is applied.</param>
		/// <param name="binder">Metadata binder being used.</param>
		/// <returns>Metadata for the function.</returns>
		public EventHandlerMetadata ToMetadata(Type formType, MetadataBinder binder)
		{
			return new EventHandlerMetadata(this.Id, this.RunAt)
			{
				CustomProperties = this.GetCustomProperties(formType, binder)
			};
		}
	}
}