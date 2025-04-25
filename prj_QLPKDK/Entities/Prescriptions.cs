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
        [Column(TypeName = "decimal(18,2)")]
        public float TotalAmount { get; set; }
        public List<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
