namespace UiMetadataFramework.Basic.Response
{
	using UiMetadataFramework.Core;

	public class RedirectResponse : FormResponse
	{
		public RedirectResponse() : base("redirect")
		{
		}

		public string Form { get; set; }
	}
}