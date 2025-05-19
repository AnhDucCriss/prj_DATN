namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionDetailRequest
    {
        public string PrescriptionId { get; set; }
        public string MedicineName { get; set; } // từ input frontend
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
