namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a mapping between server types and a component.
/// </summary>
/// <typeparam name="T">Component type.</typeparam>
public abstract class ComponentBinding<T> : IComponentBinding
	where T : ComponentAttribute
{
	private readonly IList<Type> serverTypes;

	/// <summary>
	/// Initializes a new instance of the <see cref="ComponentBinding{T}"/> class.
	/// </summary>
	/// <param name="serverTypes">Types which should be mapped to the component.</param>
	/// <param name="componentType">Name of the component.</param>
	/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
	/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
	/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
	public ComponentBinding(
		IEnumerable<Type> serverTypes,
		string componentType,
		Type? metadataFactory,
		params HasConfigurationAttribute[] allowedConfigurations)
	{
		this.serverTypes = serverTypes.ToList();
		this.ComponentType = componentType;
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
		if (obj is not ComponentBinding<T> binding)
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