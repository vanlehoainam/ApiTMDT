using ApiTMDT.Models;
using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Create([FromBody] CreatePhongBan createPhongBan)
        {
            var phongBan = new PhongBan
            {
                
                TenPB = createPhongBan.TenPB,
                SDT = createPhongBan.SDT,
                Email = createPhongBan.Email,
                NgayThanhLap =  createPhongBan.NgayThanhLap,
                GhiChu = createPhongBan.GhiChu ,
                DiaChi = createPhongBan.DiaChi ,
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
                data = result.updatedPhongBan,
                message = result.message
            });
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
           
            public string TenPB { get; set; }
            public string SDT { get; set; }
           
            public string Email { get; set; }

            [DataType(DataType.Date)]
            public DateTime? NgayThanhLap { get; set; }
            public string GhiChu { get; set; }
            public string DiaChi { get; set; }


        }
    }
}
