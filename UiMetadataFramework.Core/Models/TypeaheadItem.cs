namespace UiMetadataFramework.Core.Models
{
	public class TypeaheadItem : TypeaheadItem<long>
	{
	}

	public class TypeaheadItem<T>
	{
		public string Description { get; set; }
		public string Label { get; set; }
		public bool Parent { get; set; }
		public string Tag { get; set; }
		public T Value { get; set; }
	}
}