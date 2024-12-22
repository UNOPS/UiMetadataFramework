namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents component's configuration.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public abstract class ComponentConfigurationAttribute : Attribute;