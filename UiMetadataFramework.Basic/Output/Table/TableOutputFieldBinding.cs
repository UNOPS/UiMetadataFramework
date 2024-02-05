namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Provides binding for all <see cref="IEnumerable{T}"/> properties.
/// </summary>
public class TableOutputFieldBinding : OutputFieldBinding
{
	/// <summary>
	/// Name of the client-side control which should be able to render tabular data.
	/// </summary>
	public const string ObjectListOutputControlName = "table";

	/// <inheritdoc />
	public TableOutputFieldBinding() : base(
		new[]
		{
			typeof(IEnumerable<>),
			typeof(IList<>),
			typeof(Array)
		},
		ObjectListOutputControlName)
	{
	}

	/// <inheritdoc />
	public override IDictionary<string, object?> GetCustomProperties(
		PropertyInfo property,
		OutputFieldAttribute? attribute,
		MetadataBinder binder)
	{
		var innerType = property.PropertyType.IsArray
			? property.PropertyType.GetElementType() ??
			throw new BindingException($"Cannot get element type from array '{property.PropertyType.FullName}'.")
			: property.PropertyType.GenericTypeArguments[0];

		var isKnownOutputType = binder.OutputFieldBindings.Any(t => t.Key.ImplementsClass(innerType));

		var containerType = isKnownOutputType
			? typeof(Wrapper<>).MakeGenericType(innerType)
			: innerType;

		return property.GetCustomProperties()
			.Set("Columns", binder.BindOutputFields(containerType).ToList());
	}

	private sealed class Wrapper<T>
	{
		public Wrapper(T value)
		{
			this.Value = value;
		}

		// ReSharper disable once MemberCanBePrivate.Local
		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		// ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
		[InputField]
		public T Value { get; set; }
	}
}