using System.Linq.Expressions;

namespace BroGarage.API.Shared.Extensions
{
    /// <summary>
    /// Extension methods for pagination and sorting on IQueryable
    /// </summary>
    public static class PaginationExtensions
    {
        /// <summary>
        /// Apply pagination to an IQueryable query
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="query">The query</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="pageIndex">Page index (1-based)</param>
        /// <returns>Paginated query with Skip and Take applied</returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            int skipItem = pageSize * (pageIndex - 1);
            return query.Skip(skipItem).Take(pageSize);
        }

        /// <summary>
        /// Apply dynamic sorting to an IQueryable query
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="query">The query</param>
        /// <param name="orderBy">Property name to sort by (e.g., "Name", "CreatedDate")</param>
        /// <param name="sortDirection">Sort direction: "asc" or "desc"</param>
        /// <returns>Sorted query</returns>
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string? orderBy, string sortDirection = "desc")
            where T : class
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return query;

            var type = typeof(T);
            var property = type.GetProperty(orderBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public);

            if (property == null)
                return query;

            var parameter = Expression.Parameter(type, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = sortDirection.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        /// <summary>
        /// Get pagination metadata (total rows, total pages)
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="totalRow">Total number of rows</param>
        /// <param name="pageSize">Items per page</param>
        /// <param name="pageIndex">Current page index</param>
        /// <returns>Tuple of (totalPage, skipItem)</returns>
        public static (int TotalPage, int SkipItem) GetPaginationMetadata(int totalRow, int pageSize, int pageIndex)
        {
            int skipItem = pageSize * (pageIndex - 1);
            int totalPage = (int)Math.Ceiling((decimal)totalRow / pageSize);
            return (totalPage, skipItem);
        }
    }
}
