using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ApiTMDT.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        [Required]
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        [JsonIgnore]
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
