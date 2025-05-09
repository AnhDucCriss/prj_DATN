using   prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class MedicalRecords : BaseEntity
    {
        [ForeignKey("Patient")]
        public string PatientId { get; set; } = string.Empty;
        public DateTime ExaminationDate { get; set; } //ngày khám

        [MaxLength(100)]
        public string DoctorName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Symptoms { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Conclusion { get; set; } = string.Empty;

        public Patients? Patient { get; set; }
        public Prescriptions Prescription { get; set; } = new Prescriptions();
        public Invoices Invoice { get; set; } = new Invoices();

    }
}
