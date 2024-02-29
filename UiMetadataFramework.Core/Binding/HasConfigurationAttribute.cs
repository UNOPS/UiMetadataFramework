namespace UiMetadataFramework.Core.Binding;

using System;

/// <summary>
/// When applied to a component class will indicate a <see cref="ComponentConfigurationAttribute"/>
/// that can be used on component.
/// </summary>
/// <param name="configurationType">A class that implements <see cref="ComponentConfigurationAttribute"/>.</param>
/// <param name="name">Unique name for the configuration.</param>
/// <param name="mandatory">Indicates if this configuration is mandatory and must always be specified.</param>
/// <param name="isArray">Indicates whether the configuration will hold an array of values or a single
/// value. If set to true, then the configuration attribute can be applied multiple
/// times and the value from each attribute usage will be added to the final array,
/// which will constitute the configuration's value.</param> 
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasConfigurationAttribute(
	Type configurationType,
	bool mandatory = false,
	bool isArray = false,
	string? name = null) : Attribute
{
	/// <summary>
	/// Configuration (<see cref="ComponentConfigurationAttribute"/>) that
	/// this component supports.
	/// </summary>
	public Type ConfigurationType { get; } = configurationType.ImplementsClass(typeof(ComponentConfigurationAttribute))
		? configurationType
		: throw new BindingException(
			$"Invalid configuration type '{configurationType.Name}' specified. " +
			$"Must be a subclass of '{nameof(ComponentConfigurationAttribute)}'.");

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