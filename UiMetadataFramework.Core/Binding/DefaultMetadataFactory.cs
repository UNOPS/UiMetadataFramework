namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// <see cref="IMetadataFactory"/> that iterates over all <see cref="ComponentConfigurationAttribute"/>s,
/// finds all properties marked with <see cref="ConfigurationPropertyAttribute"/> and adds them to the metadata.
/// </summary>
public class DefaultMetadataFactory : IMetadataFactory
{
	/// <summary>
	/// Iterates over all <paramref name="configurations"/>, finds all properties marked with
	/// <see cref="ConfigurationPropertyAttribute"/> and attaches their values to the result.
	/// </summary>
	/// <param name="type">Component's type.</param>
	/// <param name="binding">Binding for the component.</param>
	/// <param name="binder">Binder to use.</param>
	/// <param name="configurations">Configurations to apply. Highest priority configs should come first.</param>
	/// <returns>Dictionary representing component's configuration.</returns>
	public object? CreateMetadata(
		Type type,
		IFieldBinding binding,
		MetadataBinder binder,
		params ComponentConfigurationAttribute[] configurations)
	{
		var result = new Dictionary<string, object?>();

		var remainingRequiredConfigs = binding.AllowedConfigurations
			.Where(t => t.Mandatory)
			.ToList();

		foreach (var configData in configurations)
		{
			var configType = configData.GetType();

			var supportedConfig = binding.AllowedConfigurations
				.SingleOrDefault(t => configType.ImplementsClass(t.ConfigurationType));

			if (supportedConfig == null)
			{
				throw new BindingException(
					$"Component '{type.Name}' does not support configurations of type '{configType.Name}'.");
			}

			var configProperties = configType
				.GetProperties()
				.Where(t => t.CanRead)
				.Select(
					t => new
					{
						Property = t,
						Info = t.GetCustomAttributeSingleOrDefault<ConfigurationPropertyAttribute>()
					})
				.Where(t => t.Info != null)
				.ToList();

			if (supportedConfig.IsArray)
			{
				result.TryGetValue(supportedConfig.Name!, out var storedList);

				var list = storedList as List<Dictionary<string, object?>> ?? [];
				var item = new Dictionary<string, object?>();

				foreach (var property in configProperties)
				{
					var propertyValue = property.Property.GetValue(configData);

					item.Add(property.Info!.Name, propertyValue);
				}

				list.Add(item);

				result[supportedConfig.Name!] = list;
			}
			else
			{
				foreach (var property in configProperties)
				{
					result.TryGetValue(property.Info!.Name, out var oldValue);

					// If the config property has already been set then we will not
					// overwrite it. That's because the configurations come in the
					// order of their priority (highest priority configs come first).
					if (oldValue == null)
					{
						var newValue = property.Property.GetValue(configData);

						if (newValue != null)
						{
							result[property.Info!.Name] = newValue;
						}
					}
				}
			}

			if (supportedConfig.Mandatory)
			{
				remainingRequiredConfigs.Remove(supportedConfig);
			}
		}

		if (remainingRequiredConfigs.Count > 0)
		{
			var missingConfigs = remainingRequiredConfigs
				.Select(t => $"'{t.ConfigurationType.Name}'")
				.JoinStrings(", ");

			throw new BindingException($"Mandatory configurations missing: {missingConfigs}.");
		}

		this.AugmentConfiguration(
			type,
			binder,
			configurations,
			result);

		return result.Count == 0
			? null
			: result;
	}

	/// <summary>
	/// Provides a hook to amend the configuration object before it is returned.
	/// </summary>
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <param name="binder">Binder to use.</param>
	/// <param name="configurationData">Configurations to apply.</param>
	/// <param name="result">Configuration prepared by <see cref="DefaultMetadataFactory"/>.
	/// This particular instance represents the final configuration object, so changes to it
	/// will be reflected in return value of <see cref="CreateMetadata"/>.</param>
	protected virtual void AugmentConfiguration(
		Type type,
		MetadataBinder binder,
		ComponentConfigurationAttribute[] configurationData,
		Dictionary<string, object?> result)
	{
	}
}