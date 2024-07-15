using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTMDT.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("GioHang")]
        public int GioHangId { get; set; }

        public virtual GioHang GioHang { get; set; }


        [ForeignKey("SanPham")]
        public int MaSP { get; set; }
        public virtual SanPhamModel SanPham { get; set; }

        [Required]
        public int SoLuong { get; set; }
    }
}
