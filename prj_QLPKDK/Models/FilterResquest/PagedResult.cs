namespace prj_QLPKDK.Models.FilterResquest
{
    public class PagedQuery
    {
        public int PageNumber { get; set; } = 1; // Mặc định trang 1
        public int PageSize { get; set; } = 10;  // Mặc định 10 item
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
