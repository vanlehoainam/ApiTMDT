using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTMDT.Models
{
    public class SanPhamModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ten_SP { get; set; }

        [Required]
        public decimal Gia { get; set; }

        public string Anh_SP { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public string MoTa { get; set; }
        public string GhiChu { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
