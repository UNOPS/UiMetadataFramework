namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// To be used inside a <see cref="ConfigurationDataAttribute"/> class to indicate
/// which properties should be serialized and added to component's configuration
/// (<see cref="Component.Configuration"/>).
/// </summary>
/// <param name="name">Name of the resulting component property.</param>
[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationPropertyAttribute(string name) : Attribute
{
	/// <summary>
	/// Name of the resulting component property.
	/// </summary>
	public string Name { get; } = name;
}