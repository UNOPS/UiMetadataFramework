namespace UiMetadataFramework.Core
{
	using System.Collections.Generic;

	public class FormMetadata
	{
		public string Id { get; set; }
		public IList<InputFieldMetadata> InputFields { get; set; }
		public string Label { get; set; }
		public IList<OutputFieldMetadata> OutputFields { get; set; }

	}
}