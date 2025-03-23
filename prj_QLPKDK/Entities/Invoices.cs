using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace   prj_QLPKDK.Entities
{
    public class Invoices : BaseEntity
    {
        public int PatientId { get; set; } // ID bệnh nhân
        public int AppointmentId { get; set; } // ID lịch hẹn

        [Required]
        public float TotalAmount { get; set; }

        [Required, StringLength(20)]
        public string PaymentStatus { get; set; } = string.Empty; // Pending, Paid, Canceled

        [Required, StringLength(20)]
        public string PaymentMethod { get; set; } = string.Empty; // Cash, Card, Insurance

    }
}
