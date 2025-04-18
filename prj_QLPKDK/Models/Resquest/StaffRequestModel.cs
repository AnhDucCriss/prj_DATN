using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class StaffRequestModel
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }
    }
}
