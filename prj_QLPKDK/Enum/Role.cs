using System.Runtime.Serialization;

namespace prj_QLPKDK.Enum
{
    public enum Role
    {
        [EnumMember(Value = "Doctor")]
        Doctor,

        [EnumMember(Value = "Nurse")]
        Nurse,

        [EnumMember(Value = "Technician")]
        Technician,

        [EnumMember(Value = "Admin")]
        Admin
    }
}
