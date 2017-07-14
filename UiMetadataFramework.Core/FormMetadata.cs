namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// Encapsulates all information needed to render a form.
	/// </summary>
	public class FormMetadata
	{
		/// <summary>
		/// Gets or sets additional parameters for the client.
		/// </summary>
		public object CustomProperties { get; set; }

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
		public string Label { get; set; }

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
	}
}