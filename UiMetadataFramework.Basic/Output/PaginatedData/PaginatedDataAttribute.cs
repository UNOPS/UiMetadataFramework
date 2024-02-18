namespace UiMetadataFramework.Basic.Output.PaginatedData;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Used for decorating <see cref="PaginatedData{T}"/> output fields, to specify which paginator
/// to use.
/// </summary>
public class PaginatedDataAttribute(string paginator) : ComponentConfigurationAttribute
{
	/// <summary>
	/// Name of the input field which will control the pagination parameters.
	/// </summary>
	public string Paginator { get; } = paginator;

	/// <inheritdoc />
	public override object? CreateMetadata(
		Type type,
		MetadataBinder binder,
		params ComponentConfigurationItemAttribute[] additionalConfigurations)
	{
		var paginatedItemType = type.GenericTypeArguments[0];

		try
		{
			var columns = binder.BindOutputFields(paginatedItemType).ToList();

			return new Properties(this.Paginator, columns);
		}
		catch (Exception ex)
		{
			throw new BindingException($"Failed to retrieve custom properties for '{type}'.", ex);
		}
	}

	/// <summary>
	/// Custom properties for the <see cref="PaginatedDataAttribute"/>.
	/// </summary>
	public class Properties(string paginator, List<OutputFieldMetadata>? columns)
	{
		/// <summary>
		/// Columns which will be displayed in the paginated data.
		/// </summary>
		public List<OutputFieldMetadata>? Columns { get; } = columns;

		/// <summary>
		/// Name of the input field which will control the pagination parameters.
		/// </summary>
		public string Paginator { get; } = paginator;
	}
}