using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class TrinhDoHocVan
    {
        [Key]
        public int MaTDHV { get; set; }
        public string TenTDHV { get; set; }

        public string TenTDNN { get; set; }
        public string GhiChu { get; set; }
    }

}
