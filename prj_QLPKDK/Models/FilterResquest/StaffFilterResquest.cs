using System.Linq.Expressions;
using prj_QLPKDK.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace prj_QLPKDK.Models.FilterResquest
{
    public class StaffFilterResquest 
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
     
