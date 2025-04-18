using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class MedicineRequestModel
    {
        [Required, MaxLength(100)]
        public string MedicineName { get; set; }

        [MaxLength(50)]
        public string Unit { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }
    }
}
