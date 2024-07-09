using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class HopDongLaoDong
    {
        [Key]
        public int MaHDLD { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }
        public virtual NhanVien NhanVien { get; set; }

        [Required]
        [StringLength(20)]
        public string LoaiHD { get; set; }
        [Required]
        public DateTime TuNgay { get; set; }

        public DateTime DenNgay { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; }

        [StringLength(500)]
        public string GhiChu { get; set; }

        public decimal LuongCoBan { get; set; }

        public DateTime NgayKy { get; set; }

        [Required]
        public DateTime NgayLap { get; set; }
    }
}
