namespace prj_QLPKDK.Models.FilterResquest
{
    public class PatientFilter
    {
        public string PatientName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
