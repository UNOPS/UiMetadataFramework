namespace UiMetadataFramework.Basic.EventHandlers
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used for decorating input fields whose values should come from an output field.
	/// </summary>
	public class BindToOutputAttribute : Attribute, IFieldEventHandlerAttribute
	{
		/// <summary>
		/// Configures default value for the input field to be a constant.
		/// </summary>
		/// <param name="outputFieldId">Id of the output field to bind to.</param>
		public BindToOutputAttribute(string outputFieldId)
		{
			this.OutputFieldId = outputFieldId;
		}

		/// <summary>
		/// Gets or sets if of the output field to bind to.
		/// </summary>
		public string OutputFieldId { get; set; }

		/// <inheritdoc />
		public string Id { get; } = "bind-to-output";

		/// <inheritdoc />
		public string RunAt { get; } = FormEvents.ResponseHandled;

		/// <inheritdoc />
		public bool ApplicableToInputField { get; } = true;

		/// <inheritdoc />
		public bool ApplicableToOutputField { get; } = false;

		/// <inheritdoc />
		public EventHandlerMetadata ToMetadata(PropertyInfo property, MetadataBinder binder)
		{
			return new EventHandlerMetadata(this.Id, this.RunAt)
			{
				CustomProperties = new Dictionary<string, object>().Set(nameof(this.OutputFieldId), this.OutputFieldId)
			};
		}
	}
}