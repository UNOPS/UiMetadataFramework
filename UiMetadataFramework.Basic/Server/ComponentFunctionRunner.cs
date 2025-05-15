namespace UiMetadataFramework.Basic.Server;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Allows running component functions defined in given assemblies.
/// </summary>
/// <remarks>Before a component function can be run, it
/// must be registered using <see cref="RegisterAssembly"/>.</remarks>
public class ComponentFunctionRunner
{
	private readonly ConcurrentDictionary<string, ComponentFunctionBinding> functions = new();
	private readonly object key = new();
	private readonly ConcurrentBag<string> registeredAssemblies = new();

	/// <summary>
	/// Scans given assembly and registers all component functions defined in it,
	/// so that they can later be invoked by this <see cref="ComponentFunctionRunner"/>.
	/// </summary>
	public void RegisterAssembly(Assembly assembly)
	{
		// Avoid registering the same assembly twice.
		lock (this.key)
		{
			if (this.registeredAssemblies.Contains(assembly.FullName))
			{
				return;
			}

			this.registeredAssemblies.Add(assembly.FullName);
		}

		var typesWithComponentFunctions = assembly.ExportedTypes
			.Where(t => t.IsClass && !t.IsAbstract)
			.Where(t => t.GetMethods().Any(c => c.GetCustomAttribute<ComponentFunctionAttribute>() != null))
			.ToList();

		foreach (var type in typesWithComponentFunctions)
		{
			foreach (var method in type.GetMethods())
			{
				var attribute = method.GetCustomAttribute<ComponentFunctionAttribute>();

				if (attribute != null)
				{
					var binding = new ComponentFunctionBinding(method, attribute);

					var name = GetName(method, binding);

					var added = this.functions.TryAdd(name, binding);

					if (!added)
					{
						throw new BindingException($"Invalid attempt to add duplicate component function `{name}`.");
					}
				}
			}
		}
	}

	/// <summary>
	/// Runs a component function with the specified name and arguments.
	/// </summary>
	/// <param name="serverComponent">Name of the server component.</param>
	/// <param name="functionName">Name of the function inside <paramref name="serverComponent"/> to run.</param>
	/// <param name="args">Args to be passed to the function.</param>
	/// <param name="sp"><see cref="IServiceProvider"/> to be used for resolving function's parameters.</param>
	/// <returns>Return value of the invoked function.</returns>
	/// <exception cref="BindingException">Thrown if function cannot be found in <see cref="functions"/>.</exception>
	public object RunFunction(
		string serverComponent,
		string functionName,
		IDictionary<string, object?> args,
		IServiceProvider sp)
	{
		var name = $"{serverComponent}.{functionName}";
		
		if (!this.functions.TryGetValue(name, out var function))
		{
			throw new BindingException($"Cannot find component function `{serverComponent}.{functionName}`.");
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

	private static string GetName(
		MethodInfo method,
		ComponentFunctionBinding binding)
	{
		var type = method.DeclaringType!;

		var serverType = type.FullName ??
			throw new BindingException($"Cannot determine full name for server component `{type.Name}`.");

		return $"{serverType}.{binding.Attribute.Name}";
	}
}