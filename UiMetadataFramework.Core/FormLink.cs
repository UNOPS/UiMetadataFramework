namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	public class FormLink
	{
		public string Form { get; set; }
		public string Id { get; set; }
		public IDictionary<string, object> InputFieldValues { get; set; }
		public string Label { get; set; }
	}
}