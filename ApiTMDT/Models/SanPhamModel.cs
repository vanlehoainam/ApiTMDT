using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiTMDT.Models
{
    public class SanPhamModel
    {
        [Key]
        public int MaSP { get; set; }

        [Required]
        public string Ten_SP { get; set; }

        [Required]
        public decimal Gia { get; set; }

        public string Anh_SP { get; set; }

        [Required]
        public int SoLuong { get; set; }

        [Required]
        public string MoTa { get; set; }

        public string GhiChu { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHang { get; set; }

        [JsonIgnore]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [JsonIgnore]
        public virtual ICollection<KhuyenMai> KhuyenMais { get; set; }  
    }
}
