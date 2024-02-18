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
	/// <returns>Metadata for component of type <paramref name="type"/>.</returns>
	public object? CreateMetadata(Type type, MetadataBinder binder);
}