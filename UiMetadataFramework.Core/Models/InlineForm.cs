namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;

	[UiModel(Name = "inline-form")]
	public class InlineForm
	{
		public string Form { get; set; }
		public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
	}
}