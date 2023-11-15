// ReSharper disable UnusedMember.Global

namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents the value of a dropdown field.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[InputFieldType(ControlName, typeof(DropdownInputFieldAttribute))]
	public class DropdownValue<T>
	{
		public const string ControlName = "dropdown";

		/// <summary>
		/// Initializes a new instance of the <see cref="DropdownValue{T}"/> class.
		/// </summary>
		public DropdownValue()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DropdownValue{T}"/> class.
		/// </summary>
		public DropdownValue(T value)
		{
			this.Value = value;
		}

		/// <summary>
		/// The value representing the selected item.
		/// </summary>
		public T? Value { get; set; }
	}
}