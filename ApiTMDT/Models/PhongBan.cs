using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class PhongBan
    {
        [Key]
        public int MaPB { get; set; }

        [Required]
        [StringLength(100)] 
        public string TenPB { get; set; }

        [Phone]
        [StringLength(10)] 
        public string SDT { get; set; }

        public int? MaTP { get; set; }

        [ForeignKey("MaTP")]
        public virtual NhanVien TruongPhong { get; set; }

    }
}
