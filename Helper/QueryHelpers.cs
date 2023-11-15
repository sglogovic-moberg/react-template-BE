using System.Linq.Expressions;

namespace ReactAppBackend.Helper
{
    public static class QueryHelpers
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> selector)
        {
            if (!condition)
            {
                return source;
            }

            return source.Where(selector);
        }

        public static bool HasValue(this string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
