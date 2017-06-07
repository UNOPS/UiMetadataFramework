namespace UiMetadataFramework.Core.Attributes
{
	using UiMetadataFramework.Core.UI.Actions;

	public class UiActionAttribute : UiPropertyAttribute
	{
		/// <summary>
		/// Creates a new instance of <see cref="UiActionAttribute"/> class.
		/// </summary>
		/// <param name="form">Name of IForm to which the action refers.</param>
		public UiActionAttribute(string form)
		{
			this.Type = "form-link";
			this.Form = form;
		}

		/// <summary>
		/// Gets or sets name of the property used to display the anchor text.
		/// </summary>
		public string AnchorText { get; set; }

		public string Form { get; set; }
		public FormLinkTarget Target { get; set; } = FormLinkTarget.Modal;
	}
}