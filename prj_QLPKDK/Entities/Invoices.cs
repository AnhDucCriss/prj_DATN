using prj_QLPKDK.Entities.BaseEntities;
using prj_QLPKDK.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace   prj_QLPKDK.Entities
{
    public class Invoices : BaseEntity
    {
        [ForeignKey("MedicalRecordId")]
        public string MedicalRecordId { get; set; }
        public MedicalRecords MedicalRecord { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public float TotalAmount { get; set; }

        
        public PaymentMethod PaymentMethod { get; set; } 

       
        public PaymentStatus PaymentStatus { get; set; }

    }
}
