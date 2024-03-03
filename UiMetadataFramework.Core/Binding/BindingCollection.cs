namespace UiMetadataFramework.Core.Binding;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Collection of <see cref="IFieldBinding"/> instances.
/// </summary>
public class BindingCollection<TBinding>
	where TBinding : IFieldBinding
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
					$"Type '{binding.ServerTypes}' is already bound to component '{binding.ClientType}'.");
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
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
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
	/// <param name="type">Component type or a <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <returns>Instance of <typeparamref name="TBinding"/> or null if the binding cannot be found.</returns>
	public TBinding? GetBindingOrNull(Type type)
	{
		var effectiveType = GetPreConfiguredComponent(type) ?? type;

		effectiveType = MetadataBinder.GetBaseComponent<ComponentAttribute>(effectiveType) ?? type;

		var componentType = effectiveType.IsArray
			? typeof(Array)
			: effectiveType.IsConstructedGenericType && !effectiveType.IsNullabble()
				? effectiveType.GetGenericTypeDefinition()
				: effectiveType;

		this.bindings.TryGetValue(componentType, out var binding);

		return binding;
	}

	/// <summary>
	/// Gets the component type T encapsulated inside <see cref="IPreConfiguredComponent{T}"/>.
	/// </summary>
	/// <param name="type">Type implementing <see cref="IPreConfiguredComponent{T}"/>.</param>
	/// <returns>Component type that is a T inside <see cref="IPreConfiguredComponent{T}"/> or null if
	/// <paramref name="type"/> is not a <see cref="IPreConfiguredComponent{T}"/>.</returns>
	/// <exception cref="BindingException">Thrown if <paramref name="type"/> implements
	/// <see cref="IPreConfiguredComponent{T}"/> in a recursive way.</exception>
	private static Type? GetPreConfiguredComponent(Type type)
	{
		var innerType = type.GetInterfaces(typeof(IPreConfiguredComponent<>))
			.SingleOrDefault()
			?.GenericTypeArguments[0];

		if (innerType == null)
		{
			return null;
		}

		if (innerType == type)
		{
			throw new BindingException(
				$"Type '{type.FullName}' implements '{typeof(IPreConfiguredComponent<>).FullName}' " +
				$"in a recursive way which is invalid.");
		}

		var recursiveInnerType = GetPreConfiguredComponent(innerType);

		if (recursiveInnerType != null)
		{
			throw new BindingException(
				$"Type '{type.FullName}' implements '{typeof(IPreConfiguredComponent<>).FullName}' " +
				$"with nested pre-configured component '{innerType.FullName}'. Nesting pre-configured " +
				$"components is not supported.");
		}

		return innerType;
	}

	private void EnforceNotDuplicate(IFieldBinding newBinding)
	{
		IFieldBinding? oldBinding = this.bindings.Values.FirstOrDefault(t => t.ClientType == newBinding.ClientType);

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
			$"Dupplicate attempts to declare component '{newBinding.ClientType}' by " +
			$"[{oldTypes}] and [{newTypes}]. Components can only be declared once.");
	}
}