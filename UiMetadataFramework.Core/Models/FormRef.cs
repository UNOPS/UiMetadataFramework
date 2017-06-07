namespace UiMetadataFramework.Core.Models
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Attributes;

	[UiModel(Name = Typename)]
	public class FormRef
	{
		public const string Typename = "form-ref";

		public string Form { get; set; }
		public string Label { get; set; }
		public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
	}
}