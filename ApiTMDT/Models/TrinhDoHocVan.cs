using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class TrinhDoHocVan
    {
        [Key]
        public int MaTDHV { get; set; }

        [Required]
        [StringLength(100)] 
        public string TenTDHV { get; set; }

            [Required]
            [StringLength(100)] 
        public string TenTDNN { get; set; }

        [StringLength(500)] 
        public string GhiChu { get; set; }
    }

}
