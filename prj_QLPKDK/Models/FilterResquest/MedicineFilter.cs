namespace prj_QLPKDK.Models.FilterResquest
{
    public class MedicineFilter
    {
        public string MedicineName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
