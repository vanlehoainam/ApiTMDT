using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ApiTMDT.Models
{
    public class HoaDon
    {
        [Key]
        public int IdHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public int KhachHangId { get; set; }

        public KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
