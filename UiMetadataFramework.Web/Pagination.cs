namespace UiMetadataFramework.Web
{
	using System;
	using System.Linq;
	using System.Linq.Expressions;
	using UiMetadataFramework.Basic.Input;
	using UiMetadataFramework.Basic.Output;

	internal static class Pagination
	{
		public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string key, bool ascending = true)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return query;
			}

			var lambda = (dynamic)CreateExpression(typeof(TSource), key);

			return ascending
				? Queryable.OrderBy(query, lambda)
				: Queryable.OrderByDescending(query, lambda);
		}

		public static PaginatedData<TSource> Paginate<TSource>(
			this IQueryable<TSource> query,
			PaginatedRequest paginationParams)
		{
			return query.Paginate(
				t => t,
				paginationParams.PageIndex,
				paginationParams.PageSize,
				paginationParams.OrderBy,
				paginationParams.Ascending);
		}

		public static PaginatedData<TSource> Paginate<TSource, TResult>(
			this IQueryable<TResult> query,
			Func<TResult, TSource> transform,
			PaginatedRequest paginationParams)
		{
			return query.Paginate(
				transform,
				paginationParams.PageIndex,
				paginationParams.PageSize,
				paginationParams.OrderBy,
				paginationParams.Ascending);
		}

		public static PaginatedData<TSource> Paginate<TSource, TResult>(
			this IQueryable<TResult> query,
			Func<TResult, TSource> transform,
			int? pageNum,
			int? pageSize,
			string orderBy = null,
			bool? ascending = true)
		{
			return query.Paginate(
				transform,
				pageNum ?? 1,
				pageSize ?? 10,
				orderBy,
				ascending ?? true);
		}

		public static PaginatedData<TSource> Paginate<TSource, TResult>(
			this IQueryable<TResult> query,
			Func<TResult, TSource> transform,
			int pageNum,
			int pageSize,
			string orderBy = null,
			bool ascending = true)
		{
			if (pageSize <= 0)
			{
				pageSize = 10;
			}

			//Total result count
			var rowsCount = query.Count();

			//If page number should be > 0 else set to first page
			if (rowsCount <= pageSize || pageNum <= 0)
			{
				pageNum = 1;
			}

			//Calculate number of rows to skip on page size
			var excludedRows = (pageNum - 1) * pageSize;

			return new PaginatedData<TSource>
			{
				Results = query
					.OrderBy(orderBy, ascending)
					.Skip(excludedRows)
					.Take(Math.Min(pageSize, rowsCount))
					.ToList()
					.Select(transform)
					.ToArray(),
				TotalCount = rowsCount
			};
		}

		private static LambdaExpression CreateExpression(Type type, string propertyName)
		{
			var param = Expression.Parameter(type, "x");

			Expression body = param;
			foreach (var member in propertyName.Split('.'))
			{
				body = Expression.PropertyOrField(body, member);
			}

			return Expression.Lambda(body, param);
		}
	}
}