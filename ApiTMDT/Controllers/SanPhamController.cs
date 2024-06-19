using ApiTMDT.Helpers;
using ApiTMDT.Models;
using ApiTMDT.Service;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var sanPhams = await _sanPhamService.GetAllSanPhamsAsync(pageNumber, pageSize);
            return Ok(sanPhams);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SanPhamModel sanPham)
        {
            var result = await _sanPhamService.CreateSanPhamAsync(sanPham);

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
                originalData = result.originalSanPham,
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
        public async Task<IActionResult> Search([FromQuery] string name, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var result = await _sanPhamService.SearchSanPhamAsync(name, minPrice, maxPrice);

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
    }
}
