namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a reference to a form.
	/// </summary>
	public class FormLink
	{
		/// <summary>
		/// Gets or sets name of the form to link to.
		/// </summary>
		public string Form { get; set; }

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object> InputFieldValues { get; set; }

		/// <summary>
		/// Gets or sets label to be shown on the client when rendering the link.
		/// </summary>
		public string Label { get; set; }
	}
}