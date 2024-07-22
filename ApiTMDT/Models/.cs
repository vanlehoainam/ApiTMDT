
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace ApiTMDT.Models
{
    public class SanPhamKhuyenMai
    {
        [Key]
        public int MaSPKM { get; set; }

        [ForeignKey("KhuyenMai")]
        public int MaKM { get; set; }

        [ForeignKey("SanPham")]
        public int MaSP { get; set; }

        [JsonIgnore]
        public virtual KhuyenMai KhuyenMai { get; set; }

        [JsonIgnore]
        public virtual SanPhamModel SanPham { get; set; }
        public virtual ICollection<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
    }
}
