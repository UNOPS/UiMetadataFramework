namespace UiMetadataFramework.Core.UI.Actions
{
	using UiMetadataFramework.Core.Metadata;

	public class FormLinkProperty : PropertyMetadata, IFormLink
	{
		public FormLinkProperty(string form, string name, params FormParameter[] parameters) : base(name, "form-link", parameters)
		{
			this.Form = form;
		}

		/// <summary>
		/// Gets or sets name of the property used to display the anchor text.
		/// </summary>
		public string AnchorText { get; set; }

		public bool Disabled { get; set; }

		public string Form { get; }
		public FormLinkTarget Target { get; set; } = FormLinkTarget.Modal;

		public string Link { get; set; }

		public static FormLinkProperty Create<TForm>(string name, params FormParameter[] parameters)
			where TForm : IFormMetadata
		{
			return new FormLinkProperty(typeof(TForm).Name, name, parameters);
		}

		public static FormLinkProperty Create<TForm>(string name, FormLinkTarget target, params FormParameter[] parameters)
			where TForm : IFormMetadata
		{
			return new FormLinkProperty(typeof(TForm).Name, name, parameters)
			{
				Target = target
			};
		}
	}
}