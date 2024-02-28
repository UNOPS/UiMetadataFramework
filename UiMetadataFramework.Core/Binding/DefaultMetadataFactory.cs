namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// <see cref="IMetadataFactory"/> that iterates over all <see cref="ConfigurationDataAttribute"/>s,
/// finds all properties marked with <see cref="ConfigurationPropertyAttribute"/> and adds them to the metadata.
/// </summary>
public class DefaultMetadataFactory : IMetadataFactory
{
	private static readonly ConcurrentDictionary<Type, HasConfigurationAttribute[]> AllowedConfigurationItems = new();

	/// <summary>
	/// Iterates over all <see cref="configurationData"/>, finds all properties marked with
	/// <see cref="ConfigurationPropertyAttribute"/> and attaches their values to the result.
	/// </summary>
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <param name="binder">Binder to use.</param>
	/// <param name="configurationData">Configurations to apply.</param>
	/// <returns>Dictionary representing component's configuration.</returns>
	public object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ConfigurationDataAttribute[] configurationData)
	{
		var result = new Dictionary<string, object?>();

		var availableConfigs = GetAvailableComponentConfigs(type);

		var remainingRequiredConfigs = availableConfigs
			.Where(t => t.Mandatory)
			.ToList();

		foreach (var configData in configurationData)
		{
			var configType = configData.GetType();

			var config = availableConfigs
				.SingleOrDefault(t => t.ConfigurationType == configType);

			if (config == null)
			{
				throw new BindingException(
					$"Invalid configuration item '{configType.Name}' attached to '{type.Name}'.");
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

			if (config.IsArray)
			{
				result.TryGetValue(config.Name!, out var storedList);

				var list = storedList as List<Dictionary<string, object?>> ?? [];
				var item = new Dictionary<string, object?>();

				foreach (var property in configProperties)
				{
					var propertyValue = property.Property.GetValue(configData);

					item.Add(property.Info!.Name, propertyValue);
				}

				list.Add(item);

				result[config.Name!] = list;
			}
			else
			{
				foreach (var property in configProperties)
				{
					var propertyValue = property.Property.GetValue(configData);

					if (propertyValue != null)
					{
						result[property.Info!.Name] = propertyValue;
					}
					else if (result.ContainsKey(property.Info!.Name))
					{
						result.Remove(property.Info!.Name);
					}
				}
			}

			if (config.Mandatory)
			{
				remainingRequiredConfigs.Remove(config);
			}
		}

		if (remainingRequiredConfigs.Count > 0)
		{
			var missingConfigs = remainingRequiredConfigs
				.Select(t => $"'{t.ConfigurationType.Name}'")
				.JoinStrings(", ");

			throw new BindingException($"Mandatory configurations missing: {missingConfigs}.");
		}

		this.AugmentConfiguration(type, binder, configurationData, result);

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
		ConfigurationDataAttribute[] configurationData,
		Dictionary<string, object?> result)
	{
	}

	/// <summary>
	/// Gets list of <see cref="HasConfigurationAttribute"/> applied to this component.
	/// </summary>
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	private static HasConfigurationAttribute[] GetAvailableComponentConfigs(Type type)
	{
		var innerComponent = MetadataBinder.GetInnerComponent(type);

		var effectiveType = innerComponent != null
			? innerComponent.PropertyType
			: type;

		return AllowedConfigurationItems.GetOrAdd(
			effectiveType,
			t => t.GetCustomAttributes<HasConfigurationAttribute>().ToArray());
	}
}