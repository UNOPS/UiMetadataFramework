namespace UiMetadataFramework.BasicFields.Response
{
	using UiMetadataFramework.Core;

	public class RedirectResponse : FormResponse
	{
		public RedirectResponse()
		{
			this.ResponseHandler = "redirect";
		}

		public string Form { get; set; }
	}
}