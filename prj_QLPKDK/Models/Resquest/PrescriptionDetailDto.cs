namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionDetailDto
    {
        public string MedicineName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
