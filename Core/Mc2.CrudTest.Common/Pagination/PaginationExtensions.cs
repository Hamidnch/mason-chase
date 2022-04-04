namespace Mc2.CrudTest.Common.Pagination
{
    public static class PaginationExtensions
    {
        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            T[] enumerable = source as T[] ?? source.ToArray();
            return enumerable.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> ToPaged<T>(this IEnumerable<T> source, int page, int pageSize, out int rowCount)
        {
            T[] enumerable = source as T[] ?? source.ToArray();
            rowCount = enumerable.Count();
            return enumerable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
