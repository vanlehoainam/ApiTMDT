using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly KhuyenMaiService _khuyenMaiService;

        public KhuyenMaiController(KhuyenMaiService khuyenMaiService)
        {
            _khuyenMaiService = khuyenMaiService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var khuyenMais = await _khuyenMaiService.GetAllKhuyenMaisAsync();
            return Ok(khuyenMais);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var khuyenMai = await _khuyenMaiService.GetKhuyenMaiByIdAsync(id);
            if (khuyenMai == null)
            {
                return NotFound(new { message = "Khuyến mãi không tồn tại." });
            }
            return Ok(khuyenMai);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] KhuyenMai khuyenMai)
        {
            var result = await _khuyenMaiService.CreateKhuyenMaiAsync(khuyenMai);
            return Ok(new
            {
                data = result.khuyenMai,
                message = result.message
            });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KhuyenMai khuyenMai)
        {
            var result = await _khuyenMaiService.UpdateKhuyenMaiAsync(id, khuyenMai);
            if (result.khuyenMai == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.khuyenMai,
                message = result.message
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _khuyenMaiService.DeleteKhuyenMaiAsync(id);
            if (!result.success)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new { message = result.message });
        }
    }
}
