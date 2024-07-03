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
        public async Task<IActionResult> GetAllNghiPhep(int pageNumber = 1, int pageSize = 5)
        {
            var nghiPheps = await _nghiPhepService.GetAllNghiPhepsAsync(pageNumber, pageSize);
            return Ok(nghiPheps);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] CreateNghiPhep createNghiPhep)
        {
            var nghiPhep = new NghiPhep
            {
                id = createNghiPhep.Id,
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
            public string Id { get; set; }
            public DateTime NgayBatDau { get; set; }
            public DateTime NgayKetThuc { get; set; }
            public string LyDo { get; set; }
            public bool? TrangThai { get; set; }
            public int MaNV { get; set; }
        }
    }
}
}
