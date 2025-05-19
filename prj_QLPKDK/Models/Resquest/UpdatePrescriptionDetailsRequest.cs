namespace prj_QLPKDK.Models.Resquest
{
    public class UpdatePrescriptionDetailsRequest
    {
        public string MedicalRecordId { get; set; }
        public List<PrescriptionDetailItemModel> Items { get; set; }
    }
}
