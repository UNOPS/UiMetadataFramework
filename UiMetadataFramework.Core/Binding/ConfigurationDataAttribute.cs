namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents configuration data that will be passed to <see cref="IMetadataFactory"/>
/// when creating metadata for a component. Unlike <see cref="ComponentConfigurationAttribute"/>,
/// a component may have multiple <see cref="ConfigurationDataAttribute"/>s.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class ConfigurationDataAttribute : Attribute;