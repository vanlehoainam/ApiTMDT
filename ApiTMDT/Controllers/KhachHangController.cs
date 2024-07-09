using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly KhachHangSevice _khachHangService;
        private readonly ApiDbContext _context;

        public KhachHangController(KhachHangSevice khachHangService, ApiDbContext context)
        {
            _khachHangService = khachHangService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var khachHangs = await _khachHangService.GetAllKhachHangsAsync();
            return Ok(khachHangs);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateKhachHang createKhachHang)
        {
            var khachHang = new KhachHang
            {
                HoTen = createKhachHang.HoTen,
                DiaChi = createKhachHang.DiaChi,
                SoDienThoai = createKhachHang.SoDienThoai,
                Email = createKhachHang.Email
            };

            var result = await _khachHangService.CreateKhachHangAsync(khachHang);

            if (result.khachHang == null)
            {
                return BadRequest(new { message = result.message });
            }

            var khachHangWithDetails = await _context.KhachHangs
                .Include(kh => kh.HoaDons)
                .FirstOrDefaultAsync(kh => kh.MaKH == khachHang.MaKH);

            return Ok(new
            {
                data = new
                {
                    khachHang = khachHangWithDetails,
                    hoaDons = khachHangWithDetails.HoaDons
                },
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] KhachHang khachHang)
        {
            var result = await _khachHangService.UpdateKhachHangAsync(id, khachHang);

            if (result.updatedKhachHang == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalKhachHang,
                data = result.updatedKhachHang,
                message = result.message
            });
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _khachHangService.DeleteKhachHangAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string nameOrPhone)
        {
            var result = await _khachHangService.SearchKhachHangAsync(nameOrPhone);

            if (!result.khachHangsWithOrders.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.khachHangsWithOrders,
                message = result.message
            });
        }

        public class CreateKhachHang
        {
            public string HoTen { get; set; }
            public string DiaChi { get; set; }
            public string SoDienThoai { get; set; }
            public string Email { get; set; }
        }
    }
}

