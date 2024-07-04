using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class HopDongLaoDong
    {
        [Key]
        public int MaHD { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }
        public virtual NhanVien NhanVien { get; set; }

        [Required]
        [StringLength(20)]
        public string LoaiHD { get; set; }

        public DateTime TuNgay { get; set; }

        public DateTime DenNgay { get; set; }
    }
}
