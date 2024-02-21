namespace UiMetadataFramework.Core.Binding;

using System;
using System.Linq;

/// <summary>
/// Represents mandatory component configuration and a <see cref="IMetadataFactory"/> that
/// can build the corresponding component metadata.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public abstract class ComponentConfigurationAttribute : Attribute, IMetadataFactory
{
	/// <inheritdoc />
	public abstract object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] configurationItems);

	/// <summary>
	/// Creates a clone of this configuration.
	/// </summary>
	/// <remarks>This implementation is using reflection and therefore may have
	/// subpar performance and relies on the configuration having public get/set
	/// properties. For a better performance or different behavior this method can
	/// be overriden.</remarks>
	public virtual ComponentConfigurationAttribute Clone()
	{
		var type = this.GetType();

		var clone = (ComponentConfigurationAttribute)Activator.CreateInstance(type);

		foreach (var property in type.GetProperties().Where(t => t.CanRead && t.CanWrite))
		{
			property.SetValue(clone, property.GetValue(this));
		}

		return clone;
	}

	/// <summary>
	/// Overwrites the properties of this configuration with the values from another
	/// configuration. All the properties with public get/set will be copied over.
	/// </summary>
	/// <param name="other">Another configuration which will override this one.
	/// Only properties with non-null values will be used.</param>
	/// <remarks>This implementation is using reflection and therefore may have
	/// subpar performance and relies on the configuration having public get/set
	/// properties. For a better performance or different behavior this method can
	/// be overriden.</remarks>
	public virtual void Merge(ComponentConfigurationAttribute? other)
	{
		if (other == null)
		{
			return;
		}

		var type = this.GetType();

		foreach (var property in type.GetProperties().Where(t => t.CanRead && t.CanWrite))
		{
			var valueInOther = property.GetValue(other);

			if (valueInOther != null)
			{
				property.SetValue(this, valueInOther);
			}
		}
	}
}