using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create([FromBody] NhanVien nhanVien)
        {
            var result = await _nhanVienService.CreateNhanVienAsync(nhanVien);

            if (result.nhanVien == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.nhanVien,
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

        // HocVan
        
        // HopDongLaoDong
       

        // PhongBan
        
    }
}
