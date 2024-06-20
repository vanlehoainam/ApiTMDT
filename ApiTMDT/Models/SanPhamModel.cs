using System.ComponentModel.DataAnnotations;

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

        [Required]
        public byte[] Anh_SP { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }
    }
}
