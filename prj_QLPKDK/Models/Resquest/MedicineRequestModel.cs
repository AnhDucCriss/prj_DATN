using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Models.Resquest
{
    public class MedicineRequestModel
    {
        [Required, MaxLength(100)]
        public string MedicineName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0.")]
        public float Price { get; set; }

        public int Quantity { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;
    }
}
