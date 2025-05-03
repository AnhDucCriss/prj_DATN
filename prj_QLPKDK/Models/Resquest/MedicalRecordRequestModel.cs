using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prj_QLPKDK.Models.Resquest
{
    public class MedicalRecordRequestModel
    {
        public string PatientId { get; set; } = string.Empty;
        public string PrescriptionId { get; set; } = string.Empty;
        public string InvoiceID { get; set; } = string.Empty;
        public DateTime ExaminationDate { get; set; } //ngày khám

        [MaxLength(100)]
        public string DoctorName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Symptoms { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Conclusion { get; set; } = string.Empty;
    }
}
