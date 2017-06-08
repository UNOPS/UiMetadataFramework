namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;
	using UiMetadataFramework.Core.Outputs;

	[UiModel(Name = ActionListProperty.Typename)]
	public class ActionButton
	{
		public string Form { get; set; }
		public Dictionary<string, object> FormValues { get; set; } = new Dictionary<string, object>();
		public string Label { get; set; }
	}
}