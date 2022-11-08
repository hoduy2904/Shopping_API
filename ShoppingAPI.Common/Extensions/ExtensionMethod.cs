using X.PagedList;

namespace ShoppingAPI.Common.Extensions
{
    public static class ExtensionMethod
    {
        public static async Task<X.PagedList.IPagedList<T>> ToPagedList<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            return await query.ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
