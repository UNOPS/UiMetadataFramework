namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Used to decorate input fields of type <see cref="TypeaheadValue{T}"/>.
	/// </summary>
	public class TypeaheadInputFieldAttribute : InputFieldAttribute
	{
		/// <summary>
		/// Initialises a new instance of the <see cref="TypeaheadInputFieldAttribute"/> class.
		/// </summary>
		/// <param name="source">Type which acts as datasource for the items. It must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.</param>
		public TypeaheadInputFieldAttribute(Type source)
		{
			this.Source = source;
		}
		
		/// <summary>
		/// Gets or sets source for the typeahead items. The type must implement
		/// <see cref="ITypeaheadRemoteSource"/> or <see cref="ITypeaheadInlineSource{T}"/>.
		/// </summary>
		public Type Source { get; }
	}
}