namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a mapping between server types and a component.
/// </summary>
/// <typeparam name="T">Component type.</typeparam>
public abstract class FieldBinding<T> : IFieldBinding
	where T : ComponentAttribute
{
	private readonly IList<Type> serverTypes;

	/// <summary>
	/// Initializes a new instance of the <see cref="OutputComponentBinding"/> class.
	/// </summary>
	/// <param name="serverTypes">Types which should be rendered on the client.</param>
	/// <param name="clientType">Name of the client control which will render the specified types.</param>
	/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
	/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
	/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
	public FieldBinding(
		IEnumerable<Type> serverTypes,
		string clientType,
		Type? metadataFactory,
		params HasConfigurationAttribute[] allowedConfigurations)
	{
		this.serverTypes = serverTypes.ToList();
		this.ComponentType = clientType;
		this.MetadataFactory = metadataFactory;
		this.AllowedConfigurations = allowedConfigurations;
	}

	/// <inheritdoc />
	public HasConfigurationAttribute[] AllowedConfigurations { get; }

	/// <inheritdoc />
	public IEnumerable<Type> ServerTypes => this.serverTypes;

	/// <inheritdoc />
	public string ComponentType { get; }

	/// <inheritdoc />
	public Type? MetadataFactory { get; }

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is not FieldBinding<T> binding)
		{
			return false;
		}

		return this.ComponentType == binding.ComponentType &&
			this.ServerTypes.All(t => binding.ServerTypes.Contains(t)) &&
			binding.ServerTypes.All(t => this.ServerTypes.Contains(t));
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		unchecked
		{
			return (this.ComponentType.GetHashCode() * 397) ^ this.ServerTypes.GetHashCode();
		}
	}
}