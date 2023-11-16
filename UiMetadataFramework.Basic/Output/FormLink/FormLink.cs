namespace UiMetadataFramework.Basic.Output.FormLink
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents a reference to a form.
	/// </summary>
	[OutputFieldType("formlink")]
	public class FormLink
	{
		/// <summary>
		/// Gets or sets a value indicating what to do when user
		/// clicks on the link. Available actions are listed in
		/// <see cref="FormLinkActions"/>.
		/// </summary>
		public string? Action { get; set; }

		/// <summary>
		/// Gets or sets name of the form to link to.
		/// </summary>
		public string Form { get; set; } = null!;

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object?>? InputFieldValues { get; set; }

		/// <summary>
		/// Gets or sets label to be shown on the client when rendering the link.
		/// </summary>
		public string? Label { get; set; }
	}
}