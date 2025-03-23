using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Rooms : BaseEntity
    {
        [Required, StringLength(50)]
        public string RoomNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; } // ID khoa (lưu ID dạng số, không FK)

        [Required, StringLength(20)]
        public string Status { get; set; } = string.Empty;// Available, Occupied
    }
}
