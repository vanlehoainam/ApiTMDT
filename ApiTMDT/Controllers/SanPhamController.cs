using ApiTMDT.Helpers;
using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly SanPhamService _sanPhamService;
        private readonly ApiDbContext _context;

        public SanPhamController(SanPhamService sanPhamService, ApiDbContext context)
        {
            _sanPhamService = sanPhamService;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var sanPhams = await _sanPhamService.GetAllSanPhamsAsync();
            return Ok(sanPhams);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateSP createSP)
        {
            var sanPham = new SanPhamModel
            {
                Ten_SP = createSP.Ten_SP,
                Gia = createSP.Gia,
                SoLuong = createSP.SoLuong,
                GhiChu = createSP.GhiChu ,
                MoTa = createSP.MoTa ,
                ImageFile = createSP.ImageFile
            };

            var result = await _sanPhamService.CreateSanPhamAsync(sanPham, createSP.ImageFile);

            if (result.sanPham == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.sanPham,
                message = result.message
            });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(int Id, [FromBody] SanPhamModel sanPham)
        {
            var result = await _sanPhamService.UpdateSanPhamAsync(Id, sanPham);

            if (result.updatedSanPham == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
               data = result.updatedSanPham,
                message = result.message
            });
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _sanPhamService.DeleteSanPhamAsync(Id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string nameorPrice)
        {
            var result = await _sanPhamService.SearchSanPhamAsync(nameorPrice);

            if (!result.sanPhams.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.sanPhams,
                message = result.message
            });
        }
        public class CreateSP
        {
            [Required]
            public string Ten_SP { get; set; }

            [Required]
            public decimal Gia { get; set; }

            [Required]
            public int SoLuong { get; set; }

            public string GhiChu { get; set; }

            public IFormFile ImageFile { get; set; }
            public string MoTa { get; set; }
        }
    }
}
