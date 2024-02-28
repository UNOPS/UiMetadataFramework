// ReSharper disable UnusedMember.Global

namespace UiMetadataFramework.Basic.Inputs.Dropdown
{
	using UiMetadataFramework.Basic.Inputs.Typeahead;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Represents the value of a dropdown field.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[InputComponent(ControlName, typeof(DropdownMetadataFactory))]
	[HasConfiguration(typeof(DropdownAttribute), mandatory: true)]
	[HasConfiguration(typeof(RemoteSourceArgumentDataAttribute), isArray: true, name: "Parameters")]
	public class DropdownValue<T>
	{
		internal const string ControlName = "dropdown";

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