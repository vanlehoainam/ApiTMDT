using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiTMDT.Models
{
    public class GioHang
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
