using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class  PrescriptionDetails : BaseEntity
    {
        [ForeignKey("Prescription")]
        public string PrescriptionId { get; set; } = string.Empty;

        [ForeignKey("Medicine")]
        public string MedicineId { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [MaxLength(100)]
        public string Unit { get; set; } = string.Empty;
        public Medicines Medicine { get; set; } 
    }
}
