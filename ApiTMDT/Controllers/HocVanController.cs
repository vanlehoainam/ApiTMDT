using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HocVanController : ControllerBase
    {
        private readonly HocVanService _hocVanService;

        public HocVanController(HocVanService hocVanService)
        {
            _hocVanService = hocVanService;
        }

        [HttpGet("HocVan/GetAll")]
        public async Task<IActionResult> GetAllHocVan()
        {
            var hocVans = await _hocVanService.GetAllHocVansAsync();
            return Ok(hocVans);
        }

        [HttpPost("HocVan/Create")]
        public async Task<IActionResult> CreateHocVan([FromBody] TrinhDoHocVan hocVan)
        {
            var result = await _hocVanService.CreateHocVanAsync(hocVan);

            if (result.hocVan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.hocVan,
                message = result.message
            });
        }

        [HttpPut("HocVan/Update")]
        public async Task<IActionResult> UpdateHocVan(int id, [FromBody] TrinhDoHocVan hocVan)
        {
            var result = await _hocVanService.UpdateHocVanAsync(id, hocVan);

            if (result.updatedHocVan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalHocVan,
                data = result.updatedHocVan,
                message = result.message
            });
        }

        [HttpDelete("HocVan/Delete")]
        public async Task<IActionResult> DeleteHocVan(int id)
        {
            var result = await _hocVanService.DeleteHocVanAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
    }
}
