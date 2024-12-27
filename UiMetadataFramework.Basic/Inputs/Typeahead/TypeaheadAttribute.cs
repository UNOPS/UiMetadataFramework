// ReSharper disable MemberCanBePrivate.Global

namespace UiMetadataFramework.Basic.Inputs.Typeahead
{
	using System;
	using UiMetadataFramework.Basic.Inputs.Dropdown;

	/// <summary>
	/// Used to decorate input fields of type <see cref="TypeaheadValue{T}"/>.
	/// </summary>
	public class TypeaheadAttribute : DropdownAttribute
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="TypeaheadAttribute"/> class.
		/// </summary>
		/// <param name="source">Type which acts as datasource for the items. It must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.</param>
		public TypeaheadAttribute(Type source) : base(source)
		{
			this.Source = source;
		}
	}
}