using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTMDT.Models
{
    public class NghiPhep
    {
        [Key]
        public int MaNP {get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LyDo {  get; set; }
        public bool? TrangThai { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }


    }
}
