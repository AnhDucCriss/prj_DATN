using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Staffs : BaseEntity
    { 
        public int UserId { get; set; } // ID người dùng (không FK)

        [Required, StringLength(50)]
        public string Position { get; set; } = string.Empty;// Nurse, Receptionist, Cleaner
    }
}
