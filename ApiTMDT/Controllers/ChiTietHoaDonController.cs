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
                DonGia = createChiTietHoaDon.DonGia,
                NgayTao = createChiTietHoaDon.NgayTao,
                GhiChu = createChiTietHoaDon.GhiChu
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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateChiTietHoaDon updateChiTietHoaDon)
        {
            var chiTietHoaDon = new ChiTietHoaDon
            {
                MaHD = updateChiTietHoaDon.MaHD,
                MaSP = updateChiTietHoaDon.MaSP,
                SoLuong = updateChiTietHoaDon.SoLuong,
                DonGia = updateChiTietHoaDon.DonGia,
                NgayTao = updateChiTietHoaDon.NgayTao,
                GhiChu = updateChiTietHoaDon.GhiChu
            };

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
            [Required]
            public int MaHD { get; set; }

            [Required]
            public int MaSP { get; set; }

            [Required]
            public int SoLuong { get; set; }

            [Required]
            public decimal DonGia { get; set; }

            public decimal ThanhTien => SoLuong * DonGia;

            [Required]
            [DataType(DataType.Date)]
            public DateTime NgayTao { get; set; }

            [StringLength(500)]
            public string GhiChu { get; set; }
        }

        public class UpdateChiTietHoaDon
        {
            [Required]
            public int MaHD { get; set; }

            [Required]
            public int MaSP { get; set; }

            [Required]
            public int SoLuong { get; set; }

            [Required]
            public decimal DonGia { get; set; }

            public decimal ThanhTien => SoLuong * DonGia;

            [Required]
            [DataType(DataType.Date)]
            public DateTime NgayTao { get; set; }

            [StringLength(500)]
            public string GhiChu { get; set; }
        }
    }
}
