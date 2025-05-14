namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionDetailRequest
    {
        public string prescriptionID { get; set; } = string.Empty;
        public string medicineName { get; set; } = string.Empty;
        public string unit { get; set; } = string.Empty;
        public int quantity { get; set; } 
    }
}
