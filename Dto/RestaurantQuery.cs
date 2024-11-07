namespace RestaurantWebApi.Dto
{
    public enum SortDirectionEnum
    {
        DESC,
        ASC
    }

    public class RestaurantQuery
    {
        public string? SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirectionEnum SortDirection { get; set; }
    }
}
