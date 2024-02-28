namespace UiMetadataFramework.Core.Binding;

using System;
using System.Linq;

/// <summary>
/// Represents mandatory component configuration and a <see cref="IMetadataFactory"/> that
/// can build the corresponding component metadata.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public abstract class ComponentConfigurationAttribute : Attribute, IMetadataFactory
{
	/// <inheritdoc />
	public abstract object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ConfigurationDataAttribute[] configurationData);
}