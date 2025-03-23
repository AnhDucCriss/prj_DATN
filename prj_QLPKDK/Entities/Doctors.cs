using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Doctors : BaseEntity
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Specialization { get; set; } = string.Empty;

        public int ExperienceYears { get; set; }

        [StringLength(255)]
        public string Qualification { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string Status { get; set; } = string.Empty; // Active, Inactive
    }
}
