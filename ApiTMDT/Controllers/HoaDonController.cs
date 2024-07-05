using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly HoaDonService _hoaDonService;
        private readonly ApiDbContext _context;

        public HoaDonController(HoaDonService hoaDonService, ApiDbContext context)
        {
            _hoaDonService = hoaDonService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var hoaDons = await _hoaDonService.GetAllHoaDonsAsync();
            return Ok(hoaDons);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateHoaDon createHoaDon)
        {
            var hoaDon = new HoaDon
            {
                NgayLap = createHoaDon.NgayLap,
                TongTien = createHoaDon.TongTien,
                MaKH = createHoaDon.MaKH
            };

            var result = await _hoaDonService.CreateHoaDonAsync(hoaDon);

            if (result.hoaDon == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = new
                {
                    hoaDon = result.hoaDon,
                    khachHang = result.hoaDon.KhachHang,
                    chiTietHoaDons = result.hoaDon.ChiTietHoaDons
                },
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] HoaDon hoaDon)
        {
            var result = await _hoaDonService.UpdateHoaDonAsync(id, hoaDon);

            if (result.updatedHoaDon == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalHoaDon,
                data = result.updatedHoaDon,
                message = result.message
            });
        }

      
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string maKHOrNgayLap)
        {
            var result = await _hoaDonService.SearchHoaDonAsync(maKHOrNgayLap);

            if (!result.hoaDons.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.hoaDons,
                message = result.message
            });
        }

        public class CreateHoaDon
        {
            public DateTime NgayLap { get; set; }
            public decimal TongTien { get; set; }
            public int MaKH { get; set; }
        }
    }
}

