using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class PrescriptionResquestModel
    {
        public int MyProperty { get; set; }
        public string MedicalRecordId { get; set; } = string.Empty;
        public string MedicineName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
