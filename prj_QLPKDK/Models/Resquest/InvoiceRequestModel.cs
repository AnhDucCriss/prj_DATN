using System.ComponentModel.DataAnnotations;
using prj_QLPKDK.Enum;

namespace prj_QLPKDK.Models.Resquest
{
    public class InvoiceRequestModel
    {
        public string MedicalRecordID { get; set; } = string.Empty;
        public PaymentMethod PaymentMethod { get; set; } 

        public PaymentStatus PaymentStatus { get; set; } 
    }
}
