namespace UiMetadataFramework.Core.UI.Actions
{
	using UiMetadataFramework.Core.Metadata;

	public class FormButtonProperty : PropertyMetadata
	{
		public FormButtonProperty(string form, string name, params FormParameter[] parameters) : base(name, "form-button", parameters)
		{
			this.Form = form;
		}

		public string Form { get; }
	}
}