namespace UiMetadataFramework.Basic.Response
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Reloads client forcing it to get metadata from the server and reinitialize everything.
	/// </summary>
	public class ReloadResponse : FormResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReloadResponse"/> class.
		/// </summary>
		public ReloadResponse()
		{
			this.Metadata = new FormResponseMetadata("reload");
		}

		/// <summary>
		/// Gets or sets name of the form to redirect to. If left null then the 
		/// client will remain on the same form.
		/// </summary>
		public string Form { get; set; } = null!;

		/// <summary>
		/// Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
		/// </summary>
		public IDictionary<string, object?>? InputFieldValues { get; set; }
	}
}