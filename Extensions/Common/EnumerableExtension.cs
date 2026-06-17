namespace SM.Extensions.Common;

public static class EnumerableExtension
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
    {
        return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}