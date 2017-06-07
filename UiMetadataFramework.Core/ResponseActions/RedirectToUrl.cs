namespace UiMetadataFramework.Core.ResponseActions
{
	public class RedirectToUrl : ResponseAction
	{
		public RedirectToUrl(string url) : base(nameof(RedirectToUrl))
		{
			this.Url = url;
		}

		public RedirectToUrl() : base(nameof(RedirectToUrl))
		{
		}

		public string Url { get; set; }
	}
}