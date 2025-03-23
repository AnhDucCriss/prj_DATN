using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Prescriptions : BaseEntity
    {
        public int AppointmentId { get; set; } // ID lịch hẹn

        [Required]
        public string MedicationDetails { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;   
    }
}
