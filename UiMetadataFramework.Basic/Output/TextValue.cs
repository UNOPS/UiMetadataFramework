namespace UiMetadataFramework.Basic.Output
{
	using UiMetadataFramework.Core.Binding;

	[OutputFieldType("text-value")]
	public class TextValue<T>
	{
		public TextValue()
		{
		}

		public TextValue(T value)
		{
			this.Value = value;
		}

		public T Value { get; set; }
	}
}