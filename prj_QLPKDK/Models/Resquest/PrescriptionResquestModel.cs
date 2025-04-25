using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionResquestModel
    {
        public string MedicalRecordId { get; set; } = string.Empty;
       
        public float TotalAmount { get; set; }
        public int Quantity { get; set; }
        
        public string Dosage { get; set; } = string.Empty;
  
        public string UsageInstructions { get; set; } = string.Empty;
    }
}
