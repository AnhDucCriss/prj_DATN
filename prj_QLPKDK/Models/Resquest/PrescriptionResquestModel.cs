using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionResquestModel
    {

        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string MedicalRecordId { get; set; } = string.Empty;    
        
    }
}
