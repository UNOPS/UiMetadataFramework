namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

/// <summary>
/// Represents a mapping between server types and a component.
/// </summary>
public class ComponentBinding : IComponentBinding
{
	private readonly IList<Type> serverTypes;

	/// <summary>
	/// Initializes a new instance of the <see cref="ComponentBinding"/> class.
	/// </summary>
	/// <param name="category">Component category.</param>
	/// <param name="serverType">Type which should be rendered on the client.</param>
	/// <param name="componentType">Name of the component.</param>
	/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
	/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
	/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
	public ComponentBinding(
		string category,
		Type serverType,
		string componentType,
		Type? metadataFactory,
		params HasConfigurationAttribute[] allowedConfigurations)
		: this(
			category,
			[serverType],
			componentType,
			metadataFactory,
			allowedConfigurations)
	{
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ComponentBinding"/> class.
	/// </summary>
	/// <param name="category">Component category.</param>
	/// <param name="serverTypes">Types which should be mapped to the component.</param>
	/// <param name="componentType">Name of the component.</param>
	/// <param name="metadataFactory"><see cref="IMetadataFactory"/> to use for constructing component's
	/// metadata. If null then <see cref="DefaultMetadataFactory"/> will be used.</param>
	/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
	protected ComponentBinding(
		string category,
		IEnumerable<Type> serverTypes,
		string componentType,
		Type? metadataFactory,
		params HasConfigurationAttribute[] allowedConfigurations)
	{
		this.Category = category;
		this.serverTypes = serverTypes.ToList();
		this.ComponentType = componentType;
		this.MetadataFactory = metadataFactory;
		this.AllowedConfigurations = allowedConfigurations;

		if (this.serverTypes.Count == 1)
		{
			this.Functions = this.serverTypes[0]
				.GetMethods()
				.Select(t => new
				{
					Method = t,
					Attribute = t.GetCustomAttribute<ComponentFunctionAttribute>()
				})
				.Where(t => t.Attribute != null)
				.Select(t => new ComponentFunctionBinding(t.Method, t.Attribute))
				.ToArray();

			var uniqueFunctionNames = this.Functions.Select(t => t.Attribute.Name).Count();

			if (uniqueFunctionNames != this.Functions.Length)
			{
				throw new BindingException(
					$"Component functions with duplicate " +
					$"names found in `{this.Category}.{this.ComponentType}`.");
			}
		}
		else
		{
			this.Functions = [];
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ComponentBinding"/> class.
	/// </summary>
	/// <param name="category">Component category.</param>
	/// <param name="serverTypes">Types which should be mapped to the component.</param>
	/// <param name="attribute">Component attribute.</param>
	/// <param name="allowedConfigurations">Allowed configurations for this component.</param>
	public ComponentBinding(
		string category,
		IEnumerable<Type> serverTypes,
		ComponentAttribute attribute,
		params HasConfigurationAttribute[] allowedConfigurations)
		: this(category, serverTypes, attribute.Name, attribute.MetadataFactory, allowedConfigurations)
	{
		this.AdditionalData = attribute.GetAdditionalData();
	}

	/// <summary>
	/// Component category to which this component belongs. Components
	/// within the same category must have unique names (<see cref="Type"/>).
	/// </summary>
	public string Category { get; }

	/// <inheritdoc />
	public IReadOnlyDictionary<string, object?>? AdditionalData { get; protected set; }

	/// <inheritdoc />
	public HasConfigurationAttribute[] AllowedConfigurations { get; }

	/// <inheritdoc />
	public string ComponentType { get; }

	/// <inheritdoc />
	public Type? MetadataFactory { get; }

	/// <inheritdoc />
	public IEnumerable<Type> ServerTypes => this.serverTypes;

	/// <inheritdoc />
	public ComponentFunctionBinding[] Functions { get; }

	/// <summary>
	/// Runs a component function with the specified name and arguments.
	/// </summary>
	/// <param name="name">Name of the function to run.</param>
	/// <param name="args">Args to be passed to the function.</param>
	/// <param name="sp"><see cref="IServiceProvider"/> to be used for resolving function's parameters.</param>
	/// <returns>Return value of the invoked function.</returns>
	/// <exception cref="BindingException">Thrown if function cannot be found in <see cref="Functions"/>.</exception>
	public object RunFunction(
		string name,
		IDictionary<string, object?> args,
		IServiceProvider sp)
	{
		var function = this.Functions.FirstOrDefault(t => t.Attribute.Name == name);

		if (function == null)
		{
			throw new BindingException(
				$"Function `{name}` not found in component " +
				$"`{this.Category}.{this.ComponentType}`.");
		}

		var parameters = function.Method.GetParameters();

		var argsList = new List<object?>(parameters.Length);

		foreach (var parameter in parameters)
		{
			if (args.TryGetValue(parameter.Name!, out var value))
			{
				if (value == null)
				{
					argsList.Add(null);
				}
				else
				{
					if (value.GetType() != parameter.ParameterType)
					{
						var convertedValue = JsonConvert.DeserializeObject(
							JsonConvert.SerializeObject(value),
							parameter.ParameterType);

						argsList.Add(convertedValue);
					}
					else
					{
						argsList.Add(value);
					}
				}
			}
			else
			{
				var injectedValue = sp.GetService(parameter.ParameterType);
				argsList.Add(injectedValue);
			}
		}

		return function.Method.Invoke(null, argsList.ToArray());
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is not ComponentBinding binding)
		{
			return false;
		}

		return
			this.Category != binding.Category &&
			this.ComponentType == binding.ComponentType &&
			this.ServerTypes.All(t => binding.ServerTypes.Contains(t)) &&
			binding.ServerTypes.All(t => this.ServerTypes.Contains(t));
	}

	/// <summary>
	/// Attempts to retrieve a value from <see cref="AdditionalData"/> with the specified key and of specified type.
	/// </summary>
	/// <param name="key">Key of the item in <see cref="AdditionalData"/>.</param>
	/// <typeparam name="T">Type that the value should have. If the value exists but is not of
	/// this type then the default value will be returned.</typeparam>
	/// <returns>Value from <see cref="AdditionalData"/> or default if no matching value was found.</returns>
	public T? GetAdditionalData<T>(string key)
	{
		if (this.AdditionalData == null)
		{
			return default;
		}

		if (this.AdditionalData.TryGetValue(key, out var value))
		{
			return value is T result ? result : default;
		}

		return default;
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