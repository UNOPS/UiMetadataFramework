namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using UiMetadataFramework.Core.Binding;

	[InputFieldType(ControlName)]
	public class DropdownValue<T>
	{
		public const string ControlName = "dropdown";

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