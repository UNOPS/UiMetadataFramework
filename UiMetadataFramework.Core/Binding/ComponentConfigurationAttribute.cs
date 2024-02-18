namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// <see cref="IMetadataFactory"/> through an attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
[CustomPropertyConfig(IsArray = false)]
public abstract class ComponentConfigurationAttribute : Attribute, IMetadataFactory
{
	/// <inheritdoc />
	public abstract object? CreateMetadata(
		Type type,
		MetadataBinder binder);
}