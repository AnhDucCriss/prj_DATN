using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prj_QLPKDK.Models.Resquest
{
    public class MedicalRecordRequestModel
    {
        public string PatientId { get; set; }
        public string PrescriptionId { get; set; }
        public string InvoiceID { get; set; }
        public DateTime ExaminationDate { get; set; } //ngày khám

        [MaxLength(100)]
        public string DoctorName { get; set; }

        [MaxLength(255)]
        public string Symptoms { get; set; }

        [MaxLength(255)]
        public string Conclusion { get; set; }
    }
}
