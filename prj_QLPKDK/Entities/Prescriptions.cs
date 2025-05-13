using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prj_QLPKDK.Entities
{
    public class Prescriptions : BaseEntity
    {
        [ForeignKey("MedicalRecord")]
        public string MedicalRecordId { get; set; } 
        public MedicalRecords MedicalRecord { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public List<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
