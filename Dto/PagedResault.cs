namespace RestaurantWebApi.Dto
{
    public class PagedResault<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }

        public PagedResault(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);
            ItemFrom = pageSize * (pageIndex - 1) + 1;
            ItemTo = TotalCount <= ItemFrom + pageSize - 1 ? TotalCount : ItemFrom + pageSize - 1;
        }
    }
}
