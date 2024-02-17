namespace UiMetadataFramework.Tests.Framework.Outputs.PaginatedData;

using System;
using System.Collections.Generic;
using System.Linq;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

public class PaginatedDataAttribute(string paginator) : CustomPropertyAttribute(PropertyName)
{
	public const string PropertyName = "paginated-data";
	public string Paginator { get; set; } = paginator;

	public override object GetValue(Type type, MetadataBinder binder)
	{
		var paginatedItemType = type.GenericTypeArguments[0];

		try
		{
			var columns = binder.BindOutputFields(paginatedItemType).ToList();

			return new Data(this.Paginator, columns);
		}
		catch (Exception ex)
		{
			throw new BindingException($"Failed to retrieve custom properties for '{type}'.", ex);
		}
	}

	public class Data(string paginator, List<OutputFieldMetadata>? columns)
	{
		public List<OutputFieldMetadata>? Columns { get; } = columns;
		public string Paginator { get; } = paginator;
	}
}