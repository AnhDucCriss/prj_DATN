using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Patients : BaseEntity
    {

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(10)]
        public string Gender { get; set; } = string.Empty; // Male, Female, Other

        [Phone, StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        public string Address { get; set; } = string.Empty;

        public string MedicalHistory { get; set; } = string.Empty; // Tiền sử bệnh án
    }
}
