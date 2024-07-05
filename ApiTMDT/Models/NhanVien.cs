using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNV { get; set; }

        public string HoTen { get; set; }

        public int CCCD { get; set; }
        public string DiaChi { get; set; }
               
        public string GioiTinh { get; set; }

        public string QueQuan { get; set; }

        public string NgaySinh { get; set; }

        public string SoDienThoai { get; set; }

        public string Email { get; set; }

        public int Luong { get; set; }

        [ForeignKey("TrinhDoHocVan")]
        public int? MaTDHV { get; set; }
        public virtual TrinhDoHocVan TrinhDoHocVan { get; set; }

        [ForeignKey("PhongBan")]
        public int? MaPB { get; set; }
        public virtual PhongBan PhongBan { get; set; }

        [ForeignKey("HopDongLaoDong")]
        public int? MaHD { get; set; }
        public virtual HopDongLaoDong HopDongLaoDong { get; set; }

        [ForeignKey("NghiPhep")]
        public int? MaNP { get; set; }
        public virtual NghiPhep NghiPhep { get; set; }


    }
}
