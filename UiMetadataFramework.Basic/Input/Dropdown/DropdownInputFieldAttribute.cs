namespace UiMetadataFramework.Basic.Input.Dropdown
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used to decorate input fields of type <see cref="DropdownValue{T}"/>.
	/// </summary>
	public class DropdownInputFieldAttribute : InputFieldAttribute
	{
		public DropdownInputFieldAttribute(Type source, params string[] parameters)
		{
			this.Source = source;
			this.Parameters = parameters;
		}

		/// <summary>
		/// Gets or sets list of property names inside "request" object. These "request" object
		/// properties will be serialized and sent to the typeahead source on each request.
		/// </summary>
		public string[] Parameters { get; set; }

		/// <summary>
		/// Gets or sets source for the dropdown items. The type must implement
		/// <see cref="IDropdownInlineSource"/> or <see cref="IDropdownRemoteSource"/>.
		/// </summary>
		public Type Source { get; set; }
	}
}