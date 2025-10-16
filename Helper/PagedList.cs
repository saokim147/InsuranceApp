using Microsoft.EntityFrameworkCore;

namespace InsuranceWebApp.Helper
{
    public class PagedList<T>
    {
        private PagedList(List<T> items, int count, int page, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Page = page;
            Items = items;
        }
        public List<T> Items { get; }
        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return new PagedList<T>(items, totalCount, page, pageSize);
        }

    }
}
