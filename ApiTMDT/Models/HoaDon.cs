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
        [Required]
        public decimal TongTien { get; set; }
        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }       
        public virtual KhachHang KhachHang { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }


    }
}
