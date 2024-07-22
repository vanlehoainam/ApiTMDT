using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiTMDT.Models
{
    public class KhuyenMai
    {
        [Key]
        public int MaKM { get; set; }

        [Required]
        [StringLength(100)]
        public string TenKM { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [Required]
        public DateTime NgayBatDau { get; set; }

        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Range(0, 100)]
        public decimal PhanTramGiam { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; }

        [JsonIgnore]
        public virtual ICollection<SanPhamModel> SanPhamModels { get; set; }  
    }
}
