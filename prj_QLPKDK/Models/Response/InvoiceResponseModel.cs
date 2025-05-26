namespace prj_QLPKDK.Models.Response
{
    public class InvoiceResponseModel
    {
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public DateTime ExaminationDate { get; set; }
        public string Conclusion { get; set; } = string.Empty;
        public float TotalAmout { get; set; }
        public bool PaymentStatus { get; set; } = false;
    }
}
