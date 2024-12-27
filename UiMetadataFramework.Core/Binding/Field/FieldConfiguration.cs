namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// To be used inside a <see cref="FieldAttribute"/> class to indicate
/// which properties should be serialized and added to component's configuration
/// (<see cref="FieldMetadata.Configuration"/>).
/// </summary>
/// <param name="name">Name of the resulting field property.</param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class FieldConfiguration(string name) : Attribute
{
	/// <summary>
	/// Name of the resulting field property.
	/// </summary>
	public string Name { get; } = name;
}