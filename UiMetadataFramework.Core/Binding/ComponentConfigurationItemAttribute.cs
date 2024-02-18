namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents a configuration item that will be passed to <see cref="IMetadataFactory"/>
/// when creating metadata for a component. Unlike <see cref="ComponentConfigurationAttribute"/>,
/// a component may have multiple <see cref="ComponentConfigurationItemAttribute"/>s.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
public abstract class ComponentConfigurationItemAttribute : Attribute;