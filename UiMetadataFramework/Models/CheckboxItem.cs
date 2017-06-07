namespace UiMetadataFramework.Core.Models
{
	public class CheckboxItem : CheckboxItem<bool>
	{
	}

	public class CheckboxItem<T>
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public T Value { get; set; }
	}
}