namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// To be used on a classes inheriting from <see cref="DefaultMetadataFactory"/>
/// in order to indicate which <see cref="ConfigurationDataAttribute"/>
/// are allowed for the component.
/// </summary>
/// <param name="configurationType"></param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasConfigurationAttribute(
	Type configurationType,
	bool mandatory = false,
	bool isArray = false,
	string? name = null) : Attribute
{
	/// <summary>
	/// Configuration (<see cref="ConfigurationDataAttribute"/>) that
	/// this component supports.
	/// </summary>
	public Type ConfigurationType { get; } = configurationType;

	/// <summary>
	/// Indicates whether the configuration will hold an array of values or a single
	/// value. If set to true, then the configuration attribute can be applied multiple
	/// times and the value from each attribute usage will be added to the final array,
	/// which will constitute the configuration's value.
	/// </summary>
	public bool IsArray { get; } = isArray;

	/// <summary>
	/// Indicates if this configuration is mandatory and must always be specified.
	/// </summary>
	public bool Mandatory { get; } = mandatory;

	/// <summary>
	/// Gets the name of the configuration property. If null then the value of this
	/// configuration will be added at the top level of the configuration object.
	/// </summary>
	/// <remarks>The name is mandatory for configurations that allow multiple items
	/// (<see cref="IsArray"/>).</remarks>
	public string? Name { get; } =
		name ?? (isArray
			? throw new ArgumentException(
				$"Configuration name is required because '{configurationType.Name}' " +
				$"was configured as an array configuration.")
			: null);
}