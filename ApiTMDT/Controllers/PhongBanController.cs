using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanController : ControllerBase
    {
        private readonly PhongBanService _phongBanService;

        public PhongBanController(PhongBanService phongBanService)
        {
            _phongBanService = phongBanService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPhongBan()
        {
            var phongBans = await _phongBanService.GetAllPhongBansAsync();
            return Ok(phongBans);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreatePhongBan createPhongBan)
        {
            var phongBan = new PhongBan
            {
                MaPB = createPhongBan.MaPB,
                TenPB = createPhongBan.TenPB,
                SDT = createPhongBan.SDT
            };

            var result = await _phongBanService.CreatePhongBanAsync(phongBan);

            if (result.phongBan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.phongBan,
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePhongBan(int id, [FromBody] PhongBan phongBan)
        {
            var result = await _phongBanService.UpdatePhongBanAsync(id, phongBan);

            if (result.updatedPhongBan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalPhongBan,
                data = result.updatedPhongBan,
                message = result.message
            });
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePhongBan(int id)
        {
            var result = await _phongBanService.DeletePhongBanAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchPhongBan([FromQuery] string nameOrPhone)
        {
            var result = await _phongBanService.SearchPhongBanAsync(nameOrPhone);

            if (!result.phongBans.Any())
            {
                return NotFound(new { message = result.message });
            }

            return Ok(new
            {
                data = result.phongBans,
                message = result.message
            });
        }
        public class CreatePhongBan
        {
            public int MaPB { get; set; }
            public string TenPB { get; set; }
            public string SDT { get; set; }

        }
    }
}
