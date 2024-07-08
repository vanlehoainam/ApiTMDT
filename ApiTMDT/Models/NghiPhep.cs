using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTMDT.Models
{
    public class NghiPhep
    {
        [Key]
        public int MaNP {get; set; }
        [Required]
        public DateTime NgayBatDau { get; set; }
        [Required]
        public DateTime NgayKetThuc { get; set; }

        [Required]
        [StringLength(500)]
        public string LyDo {  get; set; }
        public bool? TrangThai { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNV { get; set; }


    }
}
