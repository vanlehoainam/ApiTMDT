using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HopDongLaoDongController : ControllerBase
    {
        private readonly HopDongLaoDongService _hopDongLaoDongService;

        public HopDongLaoDongController(HopDongLaoDongService hopDongLaoDongService)
        {
            _hopDongLaoDongService = hopDongLaoDongService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllHopDongLaoDong()
        {
            var hopDongLaoDongs = await _hopDongLaoDongService.GetAllHopDongLaoDongsAsync();
            return Ok(hopDongLaoDongs);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateHopDongLaoDong createHopDongLaoDong)
        {
            var hopDongLaoDong = new HopDongLaoDong
            {
                MaHD = createHopDongLaoDong.MaHD,
                LoaiHD = createHopDongLaoDong.LoaiHD,
                TuNgay = createHopDongLaoDong.TuNgay,
                DenNgay = createHopDongLaoDong.DenNgay,
                MaNV = createHopDongLaoDong.MaNV
            };

            var result = await _hopDongLaoDongService.CreateHopDongLaoDongAsync(hopDongLaoDong);

            if (result.hopDongLaoDong == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.hopDongLaoDong,
                message = result.message
            });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateHopDongLaoDong(int id, [FromBody] HopDongLaoDong hopDongLaoDong)
        {
            var result = await _hopDongLaoDongService.UpdateHopDongLaoDongAsync(id, hopDongLaoDong);

            if (result.updatedHopDongLaoDong == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalHopDongLaoDong,
                data = result.updatedHopDongLaoDong,
                message = result.message
            });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteHopDongLaoDong(int id)
        {
            var result = await _hopDongLaoDongService.DeleteHopDongLaoDongAsync(id);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = result.Message });
        }
        public class CreateHopDongLaoDong
        {
            public int MaHD { get; set; }
            public string LoaiHD { get; set; }
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public int MaNV { get; set; }
        }
    }
}
