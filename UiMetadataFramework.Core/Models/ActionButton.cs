namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;
	using UiMetadataFramework.Core.UI.Actions;

	[UiModel(Name = ActionListProperty.Typename)]
	public class ActionButton : IFormLink
	{
		public bool Disabled { get; set; }
		public Dictionary<string, object> FormValues { get; set; } = new Dictionary<string, object>();
		public string Label { get; set; }

		public string Form { get; set; }
		public FormLinkTarget Target { get; set; } = FormLinkTarget.Action;

		/// <summary>
		/// Gets or sets link to, that can be called directly without action
		/// </summary>
		public string Link { get; set; }
	}
}