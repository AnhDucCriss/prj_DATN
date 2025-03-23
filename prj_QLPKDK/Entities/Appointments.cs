using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Appointments : BaseEntity
    {
        public int PatientId { get; set; } // ID bệnh nhân (lưu trữ dạng số, không dùng FK)
        public int DoctorId { get; set; } // ID bác sĩ

        public DateTime AppointmentDate { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = string.Empty;// Scheduled, Completed, Canceled

        public string Notes { get; set; } = string.Empty;
    }
}
