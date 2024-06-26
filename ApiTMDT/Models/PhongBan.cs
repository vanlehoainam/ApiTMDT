using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class PhongBan
    {
        
            [Key]
            public int MaPB { get; set; }

            [Required]
            public string TenPB { get; set; }

            public string SDT { get; set; }

            public int? MaTP { get; set; }

            [ForeignKey("MaTP")]
            public virtual NhanVien TruongPhong { get; set; }
        
    }
}
