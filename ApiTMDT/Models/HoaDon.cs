using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ApiTMDT.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }


        public DateTime NgayLap { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }
        public decimal TongTien { get; set; }
        public virtual KhachHang KhachHang { get; set; }


        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
