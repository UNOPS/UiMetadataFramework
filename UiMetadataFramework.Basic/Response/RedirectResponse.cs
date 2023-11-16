namespace UiMetadataFramework.Basic.Response
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Represents a response which redirects user to another form.
	/// </summary>
	public class RedirectResponse : FormResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RedirectResponse"/> class.
		/// </summary>
		public RedirectResponse() : base("redirect")
		{
		}

		/// <summary>
		/// Gets or sets name of the form to redirect to.
		/// </summary>
		public string Form { get; set; } = null!;

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object?>? InputFieldValues { get; set; }
	}
}