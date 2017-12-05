namespace UiMetadataFramework.Basic.Output
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	public class PaginatedDataOutputFieldBinding : OutputFieldBinding
	{
		public PaginatedDataOutputFieldBinding() : base(typeof(PaginatedData<>), "paginated-data")
		{
		}

		/// <inheritdoc cref="OutputFieldBinding.GetCustomProperties"/>
		public override IDictionary<string, object> GetCustomProperties(PropertyInfo property, OutputFieldAttribute attribute, MetadataBinder binder)
		{
			var paginatedItemType = property.PropertyType.GenericTypeArguments[0];

			if (!(attribute is PaginatedDataAttribute))
			{
				throw new BindingException(
					$"Property '{property.DeclaringType.FullName}.{property.Name}' must be decorated by " +
					$"'{nameof(PaginatedDataAttribute)}' attribute. '{nameof(PaginatedDataAttribute)}' attribute is mandatory " +
					$"for '{typeof(PaginatedData<>).Name}' output fields.");
			}

			return new Dictionary<string, object>
			{
				{ "Columns", binder.BindOutputFields(paginatedItemType).ToList() },
				{ "Customizations", attribute.GetCustomProperties(property, binder) }
			};
		}
	}

	/// <summary>
	/// Represents subset of data from a data store. This subset of data corresponds
	/// to single "page".
	/// </summary>
	public class PaginatedData<T>
	{
		/// <summary>
		/// Gets or sets items.
		/// </summary>
		public IEnumerable<T> Results { get; set; }

		/// <summary>
		/// Gets or sets total number of matching items in the data store.
		/// </summary>
		public int TotalCount { get; set; }
	}

	/// <summary>
	/// Used for decorating <see cref="PaginatedData{T}"/> output fields, to specify which paginator
	/// to use.
	/// </summary>
	public class PaginatedDataAttribute : OutputFieldAttribute
	{
		public PaginatedDataAttribute(string paginator)
		{
			this.Paginator = paginator;
		}

		/// <summary>
		/// Gets or sets name of the input field which will control the pagination parameters.
		/// </summary>
		public string Paginator { get; set; }

		public override IDictionary<string, object> GetCustomProperties(PropertyInfo property, MetadataBinder binder)
		{
			return base.GetCustomProperties(property, binder)
				.Set("Paginator", this.Paginator);
		}
	}
}