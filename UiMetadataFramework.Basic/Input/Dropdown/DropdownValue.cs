namespace UiMetadataFramework.Basic.Input.Dropdown
{
	public class DropdownValue<T>
	{
		public DropdownValue()
		{
		}

		public DropdownValue(T value)
		{
			this.Value = value;
		}

		public T Value { get; set; }
	}
}