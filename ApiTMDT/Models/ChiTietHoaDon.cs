using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTMDT.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int MaCTHD { get; set; }

        [ForeignKey("HoaDon")]
        public int MaHD { get; set; }
        public virtual HoaDon HoaDon { get; set; }

        [ForeignKey("SanPham")]
        public int MaSP { get; set; }
        public virtual SanPhamModel SanPham { get; set; }

        [Required] 
        public int SoLuong { get; set; }
        [Required]
        public decimal DonGia { get; set; }
    }
}
