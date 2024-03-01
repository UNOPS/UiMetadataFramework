namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Represents a collection of fields.
/// </summary>
/// <param name="binder">Metadata binder to use.</param>
public class FieldCollection<TFieldAttribute, TFieldMetadata, TBinding>(MetadataBinder binder, IServiceProvider container)
	where TBinding : IFieldBinding
	where TFieldAttribute : FieldAttribute<TBinding, TFieldMetadata>, new()
	where TFieldMetadata : IFieldMetadata
{
	/// <summary>
	/// <see cref="IServiceProvider"/> instance used when/if necessary.
	/// </summary>
	public readonly IServiceProvider Container = container;

	private readonly ConcurrentDictionary<Type, IEnumerable<TFieldMetadata>> fieldCache = new();

	/// <summary>
	/// Registered bindings.
	/// </summary>
	public BindingCollection<TBinding> Bindings { get; } = new();

	/// <summary>
	/// Builds metadata for a component represented by the property. 
	/// </summary>
	public Component BuildComponent(PropertyInfo property)
	{
		var configurations = property
			.GetCustomAttributes<ComponentConfigurationAttribute>(inherit: true)
			.ToArray();

		return this.BuildComponent(property.PropertyType, configurations);
	}

	/// <summary>
	/// Builds metadata for the component with the given configurations. 
	/// </summary>
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <param name="configurations">Configurations to apply. Highest priority configs should come first.</param>
	/// <returns>Metadata for the component.</returns>
	public Component BuildComponent(
		Type type,
		params ComponentConfigurationAttribute[] configurations)
	{
		var binding = this.Bindings.GetBinding(type);

		return this.BuildComponent(
			type,
			configurations,
			binding);
	}

	/// <summary>
	/// Builds component metadata.
	/// </summary>
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <param name="configurations">Configurations to apply. Highest priority configs should come first.</param>
	/// <param name="binding">Component's binding.</param>
	/// <returns><see cref="Component"/> instance.</returns>
	/// <exception cref="BindingException">Thrown if the supplied configuration data is invalid.</exception>
	public Component BuildComponent(
		Type type,
		ComponentConfigurationAttribute[] configurations,
		TBinding binding)
	{
		var effectiveConfigurationData = configurations;

		var innerComponent = MetadataBinder.GetInnerComponent(type);

		if (innerComponent != null)
		{
			effectiveConfigurationData = configurations
				// Inner configuration data should come last. This way we indicate
				// that inner configuration items have lower priority.
				.Concat(innerComponent.GetCustomAttributes<ComponentConfigurationAttribute>(true))
				.ToArray();
		}

		var metadataFactory = binding.MetadataFactory != null
			? (IMetadataFactory)this.Container.GetService(binding.MetadataFactory)
			: new DefaultMetadataFactory();

		try
		{
			var metadata = metadataFactory.CreateMetadata(
				type,
				binder,
				effectiveConfigurationData);

			return new Component(
				binding.ClientType,
				metadata);
		}
		catch (Exception e)
		{
			throw new BindingException($"Failed to construct metadata for '{type.FullName}'.", e);
		}
	}

	/// <summary>
	/// Builds field metadata for <paramref name="type"/>. This method does not take into account
	/// any previous cache and will always build metadata from scratch. The result will also not be stored
	/// in cache.
	/// </summary>
	/// <param name="type">Type that has a set of properties that represent fields.</param>
	/// <param name="strict">If true, then only properties decorated with <see cref="TFieldAttribute"/>
	/// will be taken into account.</param>
	/// <returns>Field metadata.</returns>
	public IEnumerable<TFieldMetadata> BuildFields(Type type, bool strict = false)
	{
		var properties = type.GetPublicProperties();

		foreach (var property in properties)
		{
			var attribute = property.GetCustomAttributeSingleOrDefault<TFieldAttribute>();

			if (strict && attribute == null)
			{
				continue;
			}

			var binding = this.GetInputBinding(
				property.PropertyType,
				$"{property.DeclaringType?.FullName}.{property.Name}");

			attribute ??= new TFieldAttribute();

			var metadata = attribute.GetMetadata(property, binding, binder);

			yield return metadata;
		}
	}

	/// <summary>
	/// Attempts to retrieve field metadata for <paramref name="type"/> from cache. If the
	/// cache yields no results then the metadata will be built and stored it in the cache
	/// for future use.
	/// </summary>
	/// <param name="type">Type that has a set of properties that represent fields.</param>
	/// <param name="strict">If true, then only properties decorated with <see cref="TFieldAttribute"/>
	/// will be taken into account.</param>
	/// <returns>Field metadata.</returns>
	public IEnumerable<TFieldMetadata> GetFields(Type type, bool strict = false)
	{
		return this.fieldCache.GetOrAdd(
			type,
			t => this.BuildFields(t, strict));
	}

	private TBinding GetInputBinding(Type type, string? location = null)
	{
		var binding = this.Bindings.GetBindingOrNull(type);

		if (binding == null)
		{
			var message = !string.IsNullOrWhiteSpace(location)
				? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any input component."
				: $"Type '{type.FullName}' is not bound to any input component.";

			throw new BindingException(message);
		}

		return binding;
	}
}