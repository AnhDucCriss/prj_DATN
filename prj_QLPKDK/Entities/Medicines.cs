﻿using prj_QLPKDK.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prj_QLPKDK.Entities
{
    public class Medicines : BaseEntity
    {
        [Required, MaxLength(100)]
        public string MedicineName { get; set; }

        [MaxLength(50)]
        public string Unit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        public ICollection<PrescriptionDetails> PrescriptionDetails { get; set; }
    }
}
