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
	/// Allowed configurations for the component.
	/// </summary>
	public HasConfigurationAttribute[] AllowedConfigurations { get; }

	/// <summary>
	/// Component which will be rendered.
	/// </summary>
	string ComponentType { get; }

	/// <summary>
	/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
	/// If null then <see cref="DefaultMetadataFactory"/> will be used.
	/// </summary>
	public Type? MetadataFactory { get; }

	/// <summary>
	/// Gets the server-side types being bound.
	/// </summary>
	public IEnumerable<Type> ServerTypes { get; }
}