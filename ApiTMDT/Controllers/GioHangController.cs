using ApiTMDT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly GioHangService _gioHangService;

        public GioHangController(GioHangService gioHangService)
        {
            _gioHangService = gioHangService;
        }

        [HttpGet("{maKH}")]
        public async Task<IActionResult> GetGioHang(int maKH)
        {
            var gioHang = await _gioHangService.GetGioHangByKhachHangIdAsync(maKH);
            if (gioHang == null)
            {
                return NotFound("Giỏ hàng không tồn tại.");
            }

            return Ok(gioHang);
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int maKH, int sanPhamId, int soLuong)
        {
            await _gioHangService.AddOrUpdateGioHangAsync(maKH, sanPhamId, soLuong);
            return Ok("Sản phẩm đã được thêm vào giỏ hàng.");
        }      
       
    }
}
