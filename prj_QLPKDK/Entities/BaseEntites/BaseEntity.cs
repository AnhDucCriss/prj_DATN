using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prj_QLPKDK.Entities.BaseEntities
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string LastModifyBy { get; set; } = string.Empty;
        public DateTime LastModifyAt { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
