using prj_QLPKDK.Entities.BaseEntities;
using prj_QLPKDK.Enum;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Users : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        
    }
    
}
