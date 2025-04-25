using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class PrescriptionDetails : BaseEntity
    {
        [ForeignKey("Prescription")]
        public string PrescriptionId { get; set; }

        [ForeignKey("Medicine")]
        public string MedicineId { get; set; }

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string Dosage { get; set; }

        [MaxLength(255)]
        public string UsageInstructions { get; set; }
        
        public Medicines Medicine { get; set; }
    }
}
