using System.Runtime.Serialization;

namespace prj_QLPKDK.Enum
{
    public enum Role
    {
        Admin = 0,
        Doctor = 1,
        Nurse = 2,
        Accountant = 3
    }
    public enum PaymentMethod
    {
        Transfer = 0,
        Cash = 1
    }
    public enum PaymentStatus
    {
        Unpaid = 0,
        Paid = 1
    }
}
