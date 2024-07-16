using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 5)
        {
            var hoaDons = await _hoaDonService.GetAllHoaDonsAsync(pageNumber, pageSize);
            return Ok(hoaDons);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateHoaDon createHoaDon)
        {
            var hoaDon = new HoaDon
            {
                NgayLap = createHoaDon.NgayLap,
                TongTien = createHoaDon.TongTien,
                MaKH = createHoaDon.MaKH,
                TrangThai = createHoaDon.TrangThai,
                PhuongThucThanhToan = createHoaDon.PhuongThucThanhToan,
                GhiChu = createHoaDon.GhiChu
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

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHoaDon updateHoaDon)
        {
            var hoaDon = new HoaDon
            {
                NgayLap = updateHoaDon.NgayLap,
                TongTien = updateHoaDon.TongTien,
                MaKH = updateHoaDon.MaKH,
                TrangThai = updateHoaDon.TrangThai,
                PhuongThucThanhToan = updateHoaDon.PhuongThucThanhToan,
                GhiChu = updateHoaDon.GhiChu
            };

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
            [Required]
            public DateTime NgayLap { get; set; }

            [Required]
            public decimal TongTien { get; set; }

            [Required]
            public int MaKH { get; set; }

            [Required]
            [StringLength(50)]
            public string TrangThai { get; set; }

            [Required]
            [StringLength(50)]
            public string PhuongThucThanhToan { get; set; }

            [StringLength(500)]
            public string GhiChu { get; set; }
        }

        public class UpdateHoaDon
        {
            [Required]
            public DateTime NgayLap { get; set; }

            [Required]
            public decimal TongTien { get; set; }

            [Required]
            public int MaKH { get; set; }

            [Required]
            [StringLength(50)]
            public string TrangThai { get; set; }

            [Required]
            [StringLength(50)]
            public string PhuongThucThanhToan { get; set; }

            [StringLength(500)]
            public string GhiChu { get; set; }
        }
    }
}
