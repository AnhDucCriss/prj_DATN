using System.Text.Json.Serialization;

namespace prj_QLPKDK.Models.FilterResquest
{
    public class MedicalRecordFilter
    {
        public string DoctorName { get; set; } = string.Empty;
        
        public DateTime? ExaminationDate { get; set; } //ngày khám
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
