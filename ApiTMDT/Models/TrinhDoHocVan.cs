using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class TrinhDoHocVan
    {
        [Key]
        public int MaTDHV { get; set; }
        [Required]
        public string TenTDHV { get; set; }
        [Required]
        public string TenTDNN { get; set; }
        public string GhiChu { get; set; }
    }

}
