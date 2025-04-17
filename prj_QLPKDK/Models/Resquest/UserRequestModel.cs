using prj_QLPKDK.Enum;

namespace prj_QLPKDK.Models.Resquest
{
    public class UserRequestModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
       
    }
}
