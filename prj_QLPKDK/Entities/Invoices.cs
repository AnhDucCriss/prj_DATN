using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace   prj_QLPKDK.Entities
{
    public class Invoices : BaseEntity
    {
        [ForeignKey("Patient")]
        public Patients Patient { get; set; }

        [ForeignKey("MedicalRecord")]
        public MedicalRecords MedicalRecord { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; }

        
        

    }
}
