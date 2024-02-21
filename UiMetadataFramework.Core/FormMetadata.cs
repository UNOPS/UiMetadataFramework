// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Encapsulates all information needed to render a form.
	/// </summary>
	public class FormMetadata
	{
		/// <summary>
		/// Creates a new instance of <see cref="FormMetadata"/> class.
		/// </summary>
		/// <param name="binder"></param>
		/// <param name="formType"> name="TForm">Type representing the form.</param>
		/// <param name="requestType">Type representing request for the form. 
		/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</param>
		/// <param name="responseType">Type representing response of the form. 
		/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</param>
		public FormMetadata(
			MetadataBinder binder,
			Type formType,
			Type requestType,
			Type responseType)
		{
			var formAttribute = formType.GetTypeInfo().GetCustomAttributeSingleOrDefault<FormAttribute>();

			if (formAttribute == null)
			{
				throw new BindingException(
					$"Type '{formType.FullName}' is not decorated with " +
					$"the mandatory '{typeof(FormAttribute).FullName}' attribute.");
			}

			var formEventHandlers = formType
				.GetCustomAttributesImplementingInterface<IFormEventHandlerAttribute>()
				.Select(t => t.ToMetadata(formType, binder))
				.ToList();

			this.Label = formAttribute.Label;
			this.Id = MetadataBinder.GetFormId(formType, formAttribute);
			this.PostOnLoad = formAttribute.PostOnLoad;
			this.PostOnLoadValidation = formAttribute.PostOnLoadValidation;
			this.CloseOnPostIfModal = formAttribute.CloseOnPostIfModal;
			this.OutputFields = binder.BuildOutputFields(responseType).ToList();
			this.InputFields = binder.BuildInputFields(requestType).ToList();
			this.CustomProperties = formAttribute.GetCustomProperties(formType).Merge(formType.GetCustomProperties(binder));
			this.EventHandlers = formEventHandlers;
		}

		/// <summary>
		/// Gets or sets value indicating how the form behaves when it was open as a modal and user submits it. If
		/// set to <code>true</code>, then whenever user submits the form the modal will be automatically closed
		/// (after receiving the response). If set to <code>false</code>, then the modal will remain open.
		/// </summary>
		public bool CloseOnPostIfModal { get; set; }

		/// <summary>
		/// Gets or sets additional parameters for the client.
		/// </summary>
		public IDictionary<string, object?>? CustomProperties { get; set; }

		/// <summary>
		/// Gets or sets event handlers for this form.
		/// </summary>
		public IList<EventHandlerMetadata>? EventHandlers { get; set; }

		/// <summary>
		/// Gets or sets id of the form, to which this metadata belongs.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets list of input fields.
		/// </summary>
		public IList<InputFieldMetadata> InputFields { get; set; }

		/// <summary>
		/// Gets or sets label for this form.
		/// </summary>
		public string? Label { get; set; }

		/// <summary>
		/// Gets or sets list of output fields.
		/// </summary>
		public IList<OutputFieldMetadata> OutputFields { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form should be auto-posted
		/// as soon as it has been loaded by the client. This can be useful for displaying
		/// reports and to generally show data without user having to post the form manually.
		/// </summary>
		public bool PostOnLoad { get; set; }

		/// <summary>
		/// Gets or sets value indicating whether the initial post (<see cref="PostOnLoad"/>) 
		/// should validate all input fields before posting.
		/// </summary>
		public bool PostOnLoadValidation { get; set; }
	}
}