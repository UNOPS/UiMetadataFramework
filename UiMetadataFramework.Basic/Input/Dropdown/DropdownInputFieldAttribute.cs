namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used to decorate input fields of type <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownInputFieldAttribute : InputFieldAttribute
	{
		public DropdownInputFieldAttribute(Type source)
		{
			this.Source = source;
		}

		/// <summary>
		/// Gets or sets source for the dropdown items. The type must implement
		/// <see cref="IDropdownInlineSource"/> or <see cref="IDropdownRemoteSource"/>.
		/// </summary>
		public Type Source { get; set; }
	}
}