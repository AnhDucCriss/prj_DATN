using prj_QLPKDK.Entities;

namespace prj_QLPKDK.Models.Response
{
    public class PrescriptionResponse
    {
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public List<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
