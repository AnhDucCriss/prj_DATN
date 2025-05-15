namespace prj_QLPKDK.Models.Resquest
{
    public class UpdatePrescriptionDetailsRequest
    {
        public string PrescriptionId { get; set; } = string.Empty;
        public List<PrescriptionDetailDto> PrescriptionDetails { get; set; }
    }
}
