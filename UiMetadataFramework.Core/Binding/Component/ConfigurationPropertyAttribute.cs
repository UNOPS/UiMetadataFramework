namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// To be used inside a <see cref="ComponentConfigurationAttribute"/> or <see cref="ComponentAttribute"/>
/// class to indicate which properties should be treated as configurations.
/// </summary>
/// <param name="name">Name of the resulting configuration property.</param>
[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationPropertyAttribute(string name) : Attribute
{
	/// <summary>
	/// Name of the resulting component property.
	/// </summary>
	public string Name { get; } = name;
}