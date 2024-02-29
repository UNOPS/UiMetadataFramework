namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents component's configuration.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public abstract class ComponentConfigurationAttribute : Attribute;