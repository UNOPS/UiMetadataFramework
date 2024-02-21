namespace UiMetadataFramework.Basic.Output.Table;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Builds metadata for a generic component such as <see cref="IEnumerable{T}"/>
/// which can later be used to render a table.  
/// </summary>
public class TableMetadataFactory : IMetadataFactory
{
	/// <inheritdoc />
	public object CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] configurationItems)
	{
		var innerType = type.IsArray
			? type.GetElementType() ??
			throw new BindingException($"Cannot get element type from array '{type.FullName}'.")
			: type.GenericTypeArguments[0];

		var isKnownOutputType = binder.OutputBindings.Any(t => t.Key.ImplementsClass(innerType));

		var containerType = isKnownOutputType
			? typeof(Wrapper<>).MakeGenericType(innerType)
			: innerType;

		return new Properties(binder.BuildOutputFields(containerType).ToList());
	}

	/// <summary>
	/// Custom properties for the table.
	/// </summary>
	public class Properties(List<OutputFieldMetadata> columns)
	{
		/// <summary>
		/// List of columns that the table should have.
		/// </summary>
		public List<OutputFieldMetadata> Columns { get; } = columns;
	}

	private sealed class Wrapper<T>(T value)
	{
		[InputField]
		public T Value { get; set; } = value;
	}
}