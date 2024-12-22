namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// <see cref="ICustomPropertyAttribute"/> implementation for complex properties.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
[CustomPropertyConfig(IsArray = false)]
public abstract class CustomPropertyAttribute : Attribute, ICustomPropertyAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CustomPropertyAttribute"/> class.
	/// </summary>
	/// <param name="name">Name for the custom property.</param>
	protected CustomPropertyAttribute(string name)
	{
		this.Name = name;
	}

	/// <inheritdoc />
	public abstract object GetValue(Type type, MetadataBinder binder);

	/// <inheritdoc />
	public string Name { get; }
}