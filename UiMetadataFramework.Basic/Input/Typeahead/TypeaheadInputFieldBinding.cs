// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// <see cref="InputFieldBinding"/> for <see cref="TypeaheadValue{T}"/>.
	/// </summary>
	public class TypeaheadInputFieldBinding : TypeaheadInputFieldBindingBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeaheadInputFieldBinding"/> class.
		/// </summary>
		/// <param name="container">Instance of <see cref="DependencyInjectionContainer"/>.</param>
		public TypeaheadInputFieldBinding(DependencyInjectionContainer container)
			: base(typeof(TypeaheadValue<>), "typeahead", container)
		{
		}
	}
}