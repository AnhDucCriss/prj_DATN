using   prj_QLPKDK.Entities.BaseEntities;

namespace prj_QLPKDK.Entities
{
    public class MedicalRecords : BaseEntity
    {

        public int PatientId { get; set; } // ID bệnh nhân

        public string Diagnosis { get; set; } = string.Empty;
        public string Treatment { get; set; } = string.Empty;

    }
}
