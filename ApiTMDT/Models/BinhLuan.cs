using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiTMDT.Models
{
    public class BinhLuan
    {
        [Key]
        public int MaBL { get; set; }

        [Required]
        [StringLength(500)]
        public string NoiDung { get; set; }

        [Required]
        public DateTime NgayDang { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }

        [ForeignKey("SanPham")]
        public int MaSP { get; set; }

        [Range(1, 5)]
        public int DiemDanhGia { get; set; }  

        [JsonIgnore]
        public virtual KhachHang KhachHang { get; set; }

        [JsonIgnore]
        public virtual SanPhamModel SanPham { get; set; }
    }
}
