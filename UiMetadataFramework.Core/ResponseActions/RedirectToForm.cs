namespace UiMetadataFramework.Core.ResponseActions
{
	public class RedirectToForm : ResponseAction
	{
		public RedirectToForm(string form, params FormParameter[] parameters)
			: base(nameof(RedirectToForm))
		{
			this.Parameters = parameters;
			this.Form = form;
		}

		public string Form { get; set; }

		public FormParameter[] Parameters { get; }
	}
}