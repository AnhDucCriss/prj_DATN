using prj_QLPKDK.Entities;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class PatientRequestModel
    {
        public string FullName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
    }
}
