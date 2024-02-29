// ReSharper disable MemberCanBeProtected.Global

namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// Used for decorating classes which will be used as output components.
/// A binding will be created based on this attribute, when
/// <see cref="MetadataBinder.RegisterAssembly"/> is called.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
public abstract class ComponentAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
	/// </summary>
	/// <param name="name">Name of the component.</param>
	/// <param name="metadataFactory">Type that implements <see cref="IMetadataFactory"/> and which will
	/// be used to construct component metadata (or null if component does not need any custom metadata).
	/// </param>
	protected ComponentAttribute(
		string name,
		Type? metadataFactory = null)
	{
		if (metadataFactory != null &&
			!typeof(IMetadataFactory).IsAssignableFrom(metadataFactory))
		{
			throw new BindingException(
				$"Invalid configuration of output component '{name}'. '{metadataFactory.FullName}' " +
				$"must implement '{typeof(IMetadataFactory).FullName}' in order to be used as a metadata factory.");
		}

		this.Name = name;
		this.MetadataFactory = metadataFactory;
	}

	/// <summary>
	/// Represents <see cref="IMetadataFactory"/> that should be used to construct
	/// component metadata. If null then component does not need any custom metadata.
	/// </summary>
	public Type? MetadataFactory { get; }

	/// <summary>
	/// Component name.
	/// </summary>
	public string Name { get; set; }
}