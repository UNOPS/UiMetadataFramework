namespace UiMetadataFramework.Basic.Response
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;

	public class RedirectResponse : FormResponse
	{
		public RedirectResponse() : base("redirect")
		{
		}

		/// <summary>
		/// Gets or sets name of the form to redirect to.
		/// </summary>
		public string Form { get; set; }

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object> InputFieldValues { get; set; }
	}
}