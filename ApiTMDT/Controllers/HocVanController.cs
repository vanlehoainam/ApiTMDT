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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllHocVan()
        {
            var hocVans = await _hocVanService.GetAllHocVansAsync();
            return Ok(hocVans);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateHocVan createHocVan)
        {
            var hocVan = new TrinhDoHocVan
            {
                MaTDHV = createHocVan.MaTDHV,
                TenTDHV = createHocVan.TenTDHV,
                TenTDNN = createHocVan.TenTDNN,
                GhiChu = createHocVan.GhiChu
            };

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

        [HttpPut("Update")]
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

        public class CreateHocVan
        {
            public int MaTDHV { get; set; }
            public string TenTDHV { get; set; }
            public string TenTDNN { get; set; }
            public string GhiChu { get; set; }
        }
    }
}
