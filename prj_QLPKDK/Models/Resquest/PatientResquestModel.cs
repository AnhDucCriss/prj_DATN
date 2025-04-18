using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class PatientResquestModel
    {
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
        public ICollection<MedicalRecords> MedicalRecords { get; set; }
        public ICollection<Invoices> Invoices { get; set; }
    }
}
