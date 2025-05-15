namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// When applied to a method inside a component will
/// indicate this method can be invoked by a client.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = true)]
public class ComponentFunctionAttribute : Attribute
{
	/// <summary>
	/// Represents an attribute used to annotate a method within a component,
	/// indicating that the method can be invoked by a client.
	/// </summary>
	public ComponentFunctionAttribute(string name)
	{
		this.Name = name;
	}

	/// <summary>
	/// Locally unique (within component) name for the function.
	/// </summary>
	public string Name { get; }
}