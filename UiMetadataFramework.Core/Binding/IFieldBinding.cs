namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Represents a binding between a <see cref="Type"/> of field and the client-side
/// control which will render that field. The binding can involve multiple server-side
/// types being bound to the same client-side control.
/// </summary>
public interface IFieldBinding
{
	/// <summary>
	/// Represents <see cref="IMetadataFactory"/> that should be used to construct metadata.
	/// If null then no custom metadata will be constructed.
	/// </summary>
	/// <remarks>If the type implements <see cref="ComponentConfigurationAttribute"/> then it will indicate
	/// that this component has configuration that must be provided whenever constructing its metadata.</remarks>
	public Type? MetadataFactory { get; }

	/// <summary>
	/// Gets name of the UI component which will render the field.
	/// </summary>
	string ClientType { get; }
}