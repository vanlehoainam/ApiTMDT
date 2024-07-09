using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> Create([FromBody] CreateHopDongLaoDong createHopDongLaoDong)
        {
            var hopDongLaoDong = new HopDongLaoDong
            {
              
                LoaiHD = createHopDongLaoDong.LoaiHD,
                TuNgay = createHopDongLaoDong.TuNgay,
                DenNgay = createHopDongLaoDong.DenNgay,
                MaNV = createHopDongLaoDong.MaNV,
                GhiChu = createHopDongLaoDong .GhiChu,
                TrangThai = createHopDongLaoDong.TrangThai,
                LuongCoBan = createHopDongLaoDong.LuongCoBan,
                NgayKy = createHopDongLaoDong.NgayKy,
                NgayLap=  createHopDongLaoDong.NgayLap


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

        public class CreateHopDongLaoDong
        {
            
            public string LoaiHD { get; set; }
            public DateTime TuNgay { get; set; }
            public DateTime DenNgay { get; set; }
            public int MaNV { get; set; }
            public string TrangThai { get; set; }
            public string GhiChu { get; set; }

            public decimal LuongCoBan { get; set; }

            public DateTime NgayKy { get; set; }
            public DateTime NgayLap { get; set; }
        }
    }
}
