using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NghiPhepController : ControllerBase
    {
        private readonly NghiPhepService _nghiPhepService;

        public NghiPhepController(NghiPhepService nghiPhepService)
        {
            _nghiPhepService = nghiPhepService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllNghiPhep()
        {
            var nghiPheps = await _nghiPhepService.GetAllNghiPhepsAsync();
            return Ok(nghiPheps);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateNghiPhep createNghiPhep)
        {
            var nghiPhep = new NghiPhep
            {
               
                NgayBatDau = createNghiPhep.NgayBatDau,
                NgayKetThuc = createNghiPhep.NgayKetThuc,
                LyDo = createNghiPhep.LyDo,
                TrangThai = createNghiPhep.TrangThai,
                MaNV = createNghiPhep.MaNV
            };

            var result = await _nghiPhepService.CreateNghiPhepAsync(nghiPhep);

            if (result.nghiPhep == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.nghiPhep,
                message = result.message
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateNghiPhep(string id, [FromBody] NghiPhep nghiPhep)
        {
            var result = await _nghiPhepService.UpdateNghiPhepAsync(id, nghiPhep);

            if (result.updatedNghiPhep == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                originalData = result.originalNghiPhep,
                data = result.updatedNghiPhep,
                message = result.message
            });
        }


        public class CreateNghiPhep
        {
          
            public DateTime NgayBatDau { get; set; }
            public DateTime NgayKetThuc { get; set; }
            public string LyDo { get; set; }
            public bool? TrangThai { get; set; }
            public int MaNV { get; set; }
        }
    }
}

