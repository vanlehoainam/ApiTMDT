using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ApiTMDT.Models
{
    public class HoaDon
    {
        [Key]
        public int MaHD { get; set; }


        public DateTime NgayLap { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }
        [Required]
        public decimal TongTien { get; set; }
        public virtual KhachHang KhachHang { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
