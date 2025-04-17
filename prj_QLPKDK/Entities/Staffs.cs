using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Staffs : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(10)]
        public Gender  Gender { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Position { get; set; }

    }
}
