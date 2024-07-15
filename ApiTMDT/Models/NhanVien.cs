using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        [Required]
        [StringLength(100)] 
        public string HoTen { get; set; }

        [Required]
        public int CCCD { get; set; } 

        [StringLength(200)] 
        public string DiaChi { get; set; }

        [StringLength(10)] 
        public string GioiTinh { get; set; }

        [StringLength(100)] 
        public string QueQuan { get; set; }

        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; } 

        [Required]
        [Phone]
        [StringLength(10)] 
        public string SoDienThoai { get; set; }

        [EmailAddress]
        [StringLength(100)] 
        public string Email { get; set; }

        public int Luong { get; set; }

        [ForeignKey("TrinhDoHocVan")]
        public int? MaTDHV { get; set; }
        public virtual TrinhDoHocVan TrinhDoHocVan { get; set; }

        [ForeignKey("PhongBan")]
        public int? MaPB { get; set; }
        public virtual PhongBan PhongBan { get; set; }  
       
    }
}
