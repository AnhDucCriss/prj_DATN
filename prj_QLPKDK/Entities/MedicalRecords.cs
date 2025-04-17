using   prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class MedicalRecords : BaseEntity
    {
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public DateTime ExaminationDate { get; set; } //ngày khám

        [MaxLength(100)]
        public string DoctorName { get; set; }

        [MaxLength(255)]
        public string Symptoms { get; set; }

        [MaxLength(255)]
        public string Conclusion { get; set; }

        public Patients Patient { get; set; }
        public Prescriptions Prescription { get; set; }
        public Invoices Invoice { get; set; }

    }
}
