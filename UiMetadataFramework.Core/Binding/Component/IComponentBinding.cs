namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a binding between a <see cref="Type"/> of field and the client-side
/// control which will render that field. The binding can involve multiple server-side
/// types being bound to the same client-side control.
/// </summary>
public interface IComponentBinding
{
	/// <summary>
	/// Additional data associated with this component type.
	/// </summary>
	public IReadOnlyDictionary<string, object?>? AdditionalData { get; }

	/// <summary>
	/// Allowed configurations for the component.
	/// </summary>
	public HasConfigurationAttribute[] AllowedConfigurations { get; }

	/// <summary>
	/// Component which will be rendered.
	/// </summary>
	string ComponentType { get; }

	/// <summary>
	/// List of functions that component exposes to clients.
	/// </summary>
	public ComponentFunctionBinding[] Functions { get; }

	/// <summary>
	/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
	/// If null then <see cref="DefaultMetadataFactory"/> will be used.
	/// </summary>
	public Type? MetadataFactory { get; }

	/// <summary>
	/// Gets the server-side types being bound.
	/// </summary>
	public IEnumerable<Type> ServerTypes { get; }

	/// <summary>
	/// Runs a component function with the specified name and arguments.
	/// </summary>
	/// <param name="name">Name of the function to run.</param>
	/// <param name="args">Args to be passed to the function.</param>
	/// <param name="sp"><see cref="IServiceProvider"/> to be used for resolving function's parameters.</param>
	/// <returns>Return value of the invoked function.</returns>
	object RunFunction(
		string name,
		IDictionary<string, object?> args,
		IServiceProvider sp);
}