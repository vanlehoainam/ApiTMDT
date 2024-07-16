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
    public class BinhLuanController : ControllerBase
    {
        private readonly BinhLuanService _binhLuanService;

        public BinhLuanController(BinhLuanService binhLuanService)
        {
            _binhLuanService = binhLuanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var binhLuans = await _binhLuanService.GetAllBinhLuansAsync(pageNumber, pageSize);
            return Ok(binhLuans);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBinhLuan createBinhLuan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var binhLuan = new BinhLuan
            {
                NoiDung = createBinhLuan.NoiDung,
                NgayDang = System.DateTime.Now,
                MaKH = createBinhLuan.MaKH,
                MaSP = createBinhLuan.MaSP,
                DiemDanhGia = createBinhLuan.DiemDanhGia
            };

            var result = await _binhLuanService.CreateBinhLuanAsync(binhLuan);

            if (result.binhLuan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return CreatedAtAction(nameof(GetAll), new { id = result.binhLuan.MaBL }, result.binhLuan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBinhLuan updateBinhLuan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var binhLuan = new BinhLuan
            {
                NoiDung = updateBinhLuan.NoiDung,
                DiemDanhGia = updateBinhLuan.DiemDanhGia,
                NgayDang = updateBinhLuan.NgayDang,
                MaKH = updateBinhLuan.MaKH,
                MaSP = updateBinhLuan.MaSP
            };

            var result = await _binhLuanService.UpdateBinhLuanAsync(id, binhLuan);

            if (result.binhLuan == null)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new
            {
                data = result.binhLuan,
                message = result.message
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _binhLuanService.DeleteBinhLuanAsync(id);

            if (!result.success)
            {
                return BadRequest(new { message = result.message });
            }

            return Ok(new { message = result.message });
        }

        public class CreateBinhLuan
        {
            [Required]
            public string NoiDung { get; set; }

            [Required]
            public int MaKH { get; set; }

            [Required]
            public int MaSP { get; set; }

            [Range(1, 5)]
            public int DiemDanhGia { get; set; }
        }

        public class UpdateBinhLuan
        {
            [Required]
            public string NoiDung { get; set; }

            [Range(1, 5)]
            public int DiemDanhGia { get; set; }

            public DateTime NgayDang { get; set; }

            [Required]
            public int MaKH { get; set; }

            [Required]
            public int MaSP { get; set; }
        }
    }
}
