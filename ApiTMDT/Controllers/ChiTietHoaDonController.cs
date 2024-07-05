using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly ChiTietHoaDonService _chiTietHoaDonService;
        private readonly ApiDbContext _context;

        public ChiTietHoaDonController(ChiTietHoaDonService chiTietHoaDonService, ApiDbContext context)
        {
            _chiTietHoaDonService = chiTietHoaDonService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var chiTietHoaDons = await _chiTietHoaDonService.GetAllChiTietHoaDonsAsync();
            return Ok(chiTietHoaDons);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateChiTietHoaDon createChiTietHoaDon)
        {
            var chiTietHoaDon = new ChiTietHoaDon
            {
                MaHD = createChiTietHoaDon.MaHD,
                MaSP = createChiTietHoaDon.MaSP,
                SoLuong = createChiTietHoaDon.SoLuong,
                DonGia = createChiTietHoaDon.DonGia
            };

            var result = await _chiTietHoaDonService.CreateChiTietHoaDonAsync(chiTietHoaDon);

            if (result.chiTietHoaDon == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = new
                {
                    chiTietHoaDon = result.chiTietHoaDon,
                    hoaDon = result.chiTietHoaDon.HoaDon,
                    sanPham = result.chiTietHoaDon.SanPham
                },
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] ChiTietHoaDon chiTietHoaDon)
        {
            var result = await _chiTietHoaDonService.UpdateChiTietHoaDonAsync(id, chiTietHoaDon);

            if (result.updatedChiTietHoaDon == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalChiTietHoaDon,
                data = result.updatedChiTietHoaDon,
                message = result.message
            });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string maHDOrMaSP)
        {
            var result = await _chiTietHoaDonService.SearchChiTietHoaDonAsync(maHDOrMaSP);

            if (!result.chiTietHoaDons.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.chiTietHoaDons,
                message = result.message
            });
        }

        public class CreateChiTietHoaDon
        {
            public int MaHD { get; set; }
            public int MaSP { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
        }
    }
}

