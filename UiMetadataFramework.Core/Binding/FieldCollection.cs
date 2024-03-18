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
/// <param name="container">Container to be used when/if necessary (for example to instantiate <see cref="IMetadataFactory"/> objects).</param>
public class FieldCollection<TFieldAttribute, TFieldMetadata, TBinding>(MetadataBinder binder, IServiceProvider container)
	where TBinding : IComponentBinding
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

		return this.BuildComponent(
			property.PropertyType,
			$"{property.DeclaringType!.FullName}.{property.Name}",
			configurations);
	}

	/// <summary>
	/// Builds metadata for the component with the given configurations. 
	/// </summary>
	/// <param name="type">Component type or a derived component (aka pre-configured component).</param>
	/// <param name="location"></param>
	/// <param name="configurations">Configurations to apply. Highest priority configs should come first.</param>
	/// <returns>Metadata for the component.</returns>
	public Component BuildComponent(
		Type type,
		string? location = null,
		params ComponentConfigurationAttribute[] configurations)
	{
		var binding = this.Bindings.GetBinding(type);

		return this.BuildComponent(
			type,
			binding,
			location,
			configurations);
	}

	/// <summary>
	/// Gets fields declared on <paramref name="type"/>.
	/// </summary>
	/// <param name="type">Type that has a set of properties that represent fields.</param>
	/// <param name="strict">If true, then only properties decorated with <typeparamref name="TFieldAttribute"/>
	/// will be taken into account.</param>
	/// <param name="useCache">If true then will attempt to retrieve field metadata from cache. If not
	/// in the cache then will build the metadata and store it in cache for future calls.</param>
	/// <returns>Field metadata.</returns>
	public IEnumerable<TFieldMetadata> GetFields(
		Type type,
		bool strict = false,
		bool useCache = true)
	{
		if (useCache)
		{
			return this.fieldCache.GetOrAdd(
				type,
				t => this.BuildFieldsInternal(t, strict));
		}

		return this.BuildFieldsInternal(type, strict);
	}

	/// <summary>
	/// Builds component metadata.
	/// </summary>
	/// <param name="type">Component type or a derived component (aka pre-configured component).</param>
	/// <param name="binding">Component's binding.</param>
	/// <param name="location">Path to the field where the component is located. This parameter will
	/// be used to generate a meaningful exception message if the metadata cannot be constructed.</param>
	/// <param name="configurations">Configurations to apply. Highest priority configs should come first.</param>
	/// <returns><see cref="Component"/> instance.</returns>
	/// <exception cref="BindingException">Thrown if the supplied configuration data is invalid.</exception>
	private Component BuildComponent(
		Type type,
		TBinding binding,
		string? location = null,
		params ComponentConfigurationAttribute[] configurations)
	{
		var effectiveConfigurationData = configurations;

		var innerComponentType = MetadataBinder.GetBaseComponent<ComponentAttribute>(type);

		if (innerComponentType != null)
		{
			effectiveConfigurationData = configurations
				// Inner configuration data should come last. This way we indicate
				// that inner configuration items have lower priority.
				.Concat(type.GetCustomAttributes<ComponentConfigurationAttribute>(true))
				.ToArray();
		}

		var metadataFactory = binding.MetadataFactory != null
			? (IMetadataFactory)this.Container.GetService(binding.MetadataFactory)
			: new DefaultMetadataFactory();

		try
		{
			var metadata = metadataFactory.CreateMetadata(
				innerComponentType ?? type,
				binding,
				binder,
				effectiveConfigurationData);

			return new Component(
				binding.ComponentType,
				metadata);
		}
		catch (Exception e)
		{
			var message = !string.IsNullOrWhiteSpace(location)
				? $"Failed to construct metadata for '{location}'."
				: $"Failed to construct metadata for '{type.Name}'.";

			throw new BindingException(message, e);
		}
	}

	/// <summary>
	/// Builds field metadata for <paramref name="type"/>. This method does not take into account
	/// any previous cache and will always build metadata from scratch. The result will also not be stored
	/// in cache.
	/// </summary>
	/// <param name="type">Type that has a set of properties that represent fields.</param>
	/// <param name="strict">If true, then only properties decorated with <typeparamref name="TFieldAttribute"/>
	/// will be taken into account.</param>
	/// <returns>Field metadata.</returns>
	private IEnumerable<TFieldMetadata> BuildFieldsInternal(Type type, bool strict = false)
	{
		var properties = type.GetPublicProperties();

		foreach (var property in properties)
		{
			var attribute = property.GetCustomAttributeSingleOrDefault<TFieldAttribute>();

			if (strict && attribute == null)
			{
				continue;
			}

			var binding = this.GetBinding(
				property.PropertyType,
				$"{property.DeclaringType?.FullName}.{property.Name}");

			attribute ??= new TFieldAttribute();

			var metadata = attribute.GetMetadata(property, binding, binder);

			yield return metadata;
		}
	}

	/// <summary>
	/// Gets binding for the specified component type.
	/// </summary>
	/// <param name="type">Component type or a derived component (aka pre-configured component).</param>
	/// <param name="location">Path to the field where the component is located. This parameter will
	/// be used to generate a meaningful exception message if the binding cannot be found.</param>
	/// <returns>Instance of <typeparamref name="TBinding"/>.</returns>
	/// <exception cref="BindingException">Thrown if binding cannot be found.</exception>
	private TBinding GetBinding(Type type, string? location = null)
	{
		var binding = this.Bindings.GetBindingOrNull(type);

		if (binding == null)
		{
			var message = !string.IsNullOrWhiteSpace(location)
				? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any component."
				: $"Type '{type.FullName}' is not bound to any component.";

			throw new BindingException(message);
		}

		return binding;
	}
}