using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace prj_QLPKDK.Entities
{
    public class Patients : BaseEntity
    {

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(10)]
        public Gender Gender { get; set; }

        public int Age { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<MedicalRecords> MedicalRecords { get; set; }
        public ICollection<Invoices> Invoices { get; set; }
    }

    public enum Gender
    {
        [EnumMember(Value = "Nam")]
        Nam,

        [EnumMember(Value = "Nữ")]
        Nu,

    }
}
