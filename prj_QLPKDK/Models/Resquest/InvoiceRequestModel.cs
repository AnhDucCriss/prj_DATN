using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class InvoiceRequestModel
    {
        public int PatientId { get; set; }
        public int MedicalRecordId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Tổng tiền phải lớn hơn 0.")]
        public decimal TotalAmount { get; set; }
        [MaxLength(50, ErrorMessage = "Phương thức thanh toán không được vượt quá 50 ký tự.")]
        public string PaymentMethod { get; set; }
        [MaxLength(50, ErrorMessage = "Trạng thái thanh toán không được vượt quá 50 ký tự.")]
        public string PaymentStatus { get; set; }
    }
}
