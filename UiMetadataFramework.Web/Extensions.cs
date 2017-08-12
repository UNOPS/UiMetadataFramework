namespace UiMetadataFramework.Web
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Web.Metadata.Typeahead;

	public static class Extensions
	{
		public static bool Contains(this IEnumerable<string> values, string search, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
		{
			return values.Any(t => t.Equals(search, stringComparison));
		}

		public static bool Contains(this string value, string substring, StringComparison comparison)
		{
			return value.IndexOf(substring, comparison) >= 0;
		}

		public static TypeaheadResponse<TKey> GetForTypeahead<TItem, TKey>(
			this IQueryable<TItem> queryable,
			TypeaheadRequest<TKey> request,
			Expression<Func<TItem, TypeaheadItem<TKey>>> select,
			Expression<Func<TItem, bool>> getById,
			Expression<Func<TItem, bool>> getByQuery)
		{
			if (request.GetByIds)
			{
				var items = queryable.Where(getById).Select(select).ToList();
				return new TypeaheadResponse<TKey>
				{
					Items = items,
					TotalItemCount = items.Count
				};
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(request.Query))
				{
					queryable = queryable.Where(getByQuery);
				}

				var items = queryable.Select(select);

				return new TypeaheadResponse<TKey>
				{
					Items = items.Take(TypeaheadRequest<int>.ItemsPerRequest),
					TotalItemCount = items.Count()
				};
			}
		}
	}
}