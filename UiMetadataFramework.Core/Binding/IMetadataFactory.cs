namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents a factory for creating component metadata.
/// </summary>
public interface IMetadataFactory
{
	/// <summary>
	/// Creates metadata for a given component type.
	/// </summary>
	/// <param name="type">Component's type.</param>
	/// <param name="binder"><see cref="MetadataBinder"/> instance.</param>
	/// <param name="binding">Binding for the component.</param>
	/// <param name="configurations">Configurations data to use when constructing the metadata.
	/// In case of a derived component (aka - pre-configured component), the outer configuration items
	/// will appear first and the inner items will be at the end of the list.</param>
	/// <returns>Metadata for component of type <paramref name="type"/>.</returns>
	public object? CreateMetadata(
		Type type,
		IComponentBinding binding,
		MetadataBinder binder,
		params ComponentConfigurationAttribute[] configurations);
}