namespace UiMetadataFramework.Core.Binding
{
	using System.Collections.Generic;

	public class EnumerableOutputFieldProperties
	{
		public IList<OutputFieldMetadata> Columns { get; set; }
		public object Customizations { get; set; }
	}
}