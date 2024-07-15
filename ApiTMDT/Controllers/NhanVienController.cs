using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly NhanVienSevice _nhanVienService;
        private readonly HocVanService _hocVanService;
        private readonly HopDongLaoDongService _hopDongLaoDongService;
        private readonly PhongBanService _phongBanService;
        private readonly ApiDbContext _context;

        public NhanVienController(NhanVienSevice nhanVienService, HocVanService hocVanService, HopDongLaoDongService hopDongLaoDongService, PhongBanService phongBanService, ApiDbContext context)
        {
            _nhanVienService = nhanVienService;
            _hocVanService = hocVanService;
            _hopDongLaoDongService = hopDongLaoDongService;
            _phongBanService = phongBanService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var nhanViens = await _nhanVienService.GetAllNhanViensAsync();
            return Ok(nhanViens);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateNhanVien createNhanVien)
        {
            var nhanVien = new NhanVien
            {
                HoTen = createNhanVien.HoTen,
                CCCD = createNhanVien.CCCD,
                DiaChi = createNhanVien.DiaChi,
                GioiTinh = createNhanVien.GioiTinh,
                QueQuan = createNhanVien.QueQuan,
                NgaySinh = createNhanVien.NgaySinh,
                SoDienThoai = createNhanVien.SoDienThoai,
                Email = createNhanVien.Email,
                Luong = createNhanVien.Luong,
                MaTDHV = createNhanVien.MaTDHV,                
                MaPB = createNhanVien.MaPB,
         
            };

            var result = await _nhanVienService.CreateNhanVienAsync(nhanVien);

            if (result.nhanVien == null)
            {
                return BadRequest(new { message = result.message });
            }

            var nhanVienWithDetails = await _context.NhanVien
                .Include(nv => nv.TrinhDoHocVan)              
                .Include(nv => nv.PhongBan)
                .FirstOrDefaultAsync(nv => nv.MaNV == nhanVien.MaNV);

            return Ok(new
            {
                data = new
                {
                    nhanVien = nhanVienWithDetails,
                    trinhDoHocVan = nhanVienWithDetails.TrinhDoHocVan,                   
                    phongBan = nhanVienWithDetails.PhongBan
                },
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] NhanVien nhanVien)
        {
            var result = await _nhanVienService.UpdateNhanVienAsync(id, nhanVien);

            if (result.updatedNhanVien == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalNhanVien,
                data = result.updatedNhanVien,
                message = result.message
            });
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _nhanVienService.DeleteNhanVienAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string nameOrCCCD)
        {
            var result = await _nhanVienService.SearchNhanVienAsync(nameOrCCCD);

            if (!result.nhanViens.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.nhanViens,
                message = result.message
            });
        }
        public class CreateNhanVien
        {
            public int MaNV { get; set; }
            public string HoTen { get; set; }
            public int CCCD { get; set; }
            public string DiaChi { get; set; }
            public string GioiTinh { get; set; }
            public string QueQuan { get; set; }

            [DataType(DataType.Date)]
            public DateTime NgaySinh { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
            public int Luong { get; set; }            
            public int? MaTDHV { get; set; }         

            public int? MaPB { get; set; }  
                  
          

        }
    }
}
