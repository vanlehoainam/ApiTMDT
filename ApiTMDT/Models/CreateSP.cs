using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class CreateSP
    {
        [Required]
        public string Ten_SP { get; set; }

        [Required]
        public decimal Gia { get; set; }

        [Required]
        public int SoLuong { get; set; }

        public string GhiChu { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
