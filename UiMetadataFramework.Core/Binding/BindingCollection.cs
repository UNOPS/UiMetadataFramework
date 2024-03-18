namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Collection of <see cref="IComponentBinding"/> instances.
/// </summary>
public class BindingCollection<TBinding>
	where TBinding : IComponentBinding
{
	private readonly ConcurrentDictionary<Type, TBinding> bindings = new();

	/// <summary>
	/// Gets list of all bindings in the collection.
	/// </summary>
	public IReadOnlyDictionary<Type, TBinding> All => this.bindings.AsReadOnlyDictionary();

	/// <summary>
	/// Adds binding to the collection.
	/// </summary>
	/// <param name="binding"><typeparamref name="TBinding"/> instance.</param>
	public void AddBinding(TBinding binding)
	{
		this.EnforceNotDuplicate(binding);

		foreach (var serverType in binding.ServerTypes)
		{
			if (this.bindings.ContainsKey(serverType))
			{
				throw new InvalidOperationException(
					$"Type '{binding.ServerTypes}' is already bound to component '{binding.ComponentType}'.");
			}

			if (serverType.GetTypeInfo().IsValueType)
			{
				// Bind nullable version of the value type.
				// For example when binding "int", also bind "int?".
				var nullable = typeof(Nullable<>).MakeGenericType(serverType);

				this.bindings.TryAdd(nullable, binding);
			}

			this.bindings.TryAdd(serverType, binding);
		}
	}

	/// <summary>
	/// Retrieves binding for the specified type. If not found then an exception is thrown.
	/// </summary>
	/// <param name="type">Component type or a derived component (aka pre-configured component).</param>
	/// <param name="location">Path to the field where the component is located. This parameter will
	/// be used to generate a meaningful exception message if the binding cannot be found.</param>
	/// <returns>Instance of <typeparamref name="TBinding"/>.</returns>
	/// <exception cref="BindingException">Thrown if the binding cannot be found.</exception>
	public TBinding GetBinding(Type type, string? location = null)
	{
		var binding = this.GetBindingOrNull(type);

		if (binding == null)
		{
			var message = !string.IsNullOrWhiteSpace(location)
				? $"Cannot retrieve metadata for '{location}', because type '{type.FullName}' is not bound to any input component."
				: $"Type '{type.FullName}' is not bound to any input component.";

			throw new BindingException(message);
		}

		return binding;
	}

	/// <summary>
	/// Returns binding for the specified type or null if no such binding can be found.
	/// </summary>
	/// <param name="type">Component type or a derived component (aka pre-configured component).</param>
	/// <returns>Instance of <typeparamref name="TBinding"/> or null if the binding cannot be found.</returns>
	public TBinding? GetBindingOrNull(Type type)
	{
		var effectiveType = MetadataBinder.GetBaseComponent<ComponentAttribute>(type) ?? type;

		var componentType = effectiveType.IsArray
			? typeof(Array)
			: effectiveType.IsConstructedGenericType && !effectiveType.IsNullabble()
				? effectiveType.GetGenericTypeDefinition()
				: effectiveType;

		this.bindings.TryGetValue(componentType, out var binding);

		return binding;
	}

	private void EnforceNotDuplicate(IComponentBinding newBinding)
	{
		IComponentBinding? oldBinding = this.bindings.Values.FirstOrDefault(t => t.ComponentType == newBinding.ComponentType);

		if (oldBinding == null || oldBinding.Equals(newBinding))
		{
			return;
		}

		var oldTypes = oldBinding.ServerTypes
			.Select(t => $"{t.Name}")
			.JoinStrings(", ");

		var newTypes = newBinding.ServerTypes
			.Select(t => $"{t.Name}")
			.JoinStrings(", ");

		throw new BindingException(
			$"Dupplicate attempts to declare component '{newBinding.ComponentType}' by " +
			$"[{oldTypes}] and [{newTypes}]. Components can only be declared once.");
	}
}