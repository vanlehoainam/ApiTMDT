using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int IdCTHD { get; set; }
        public int HoaDonId { get; set; }
        public int SanPhamId { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        public HoaDon HoaDon { get; set; }
        public SanPhamModel SanPham { get; set; }
    }
}
