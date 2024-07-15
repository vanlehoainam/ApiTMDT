using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTMDT.Service
{
    public class GioHangService
    {
        private readonly ApiDbContext _context;

        public GioHangService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<GioHang> GetGioHangByKhachHangIdAsync(int maKH)
        {
            return await _context.GioHangs
                .Include(gh => gh.ChiTietGioHangs)
                .ThenInclude(ct => ct.SanPham)
                .FirstOrDefaultAsync(gh => gh.MaKH == maKH);
        }

        public async Task<string> AddToGioHangAsync(int maKH, int maSP, int soLuong)
        {
            var gioHang = await _context.GioHangs.FirstOrDefaultAsync(g => g.MaKH == maKH);
            if (gioHang == null)
            {
                gioHang = new GioHang { MaKH = maKH };
                _context.GioHangs.Add(gioHang);
                await _context.SaveChangesAsync();
            }

            var chiTietGioHang = new ChiTietGioHang
            {
                GioHangId = gioHang.Id,
                MaSP = maSP,
                SoLuong = soLuong
            };
            _context.ChiTietGioHangs.Add(chiTietGioHang);
            await _context.SaveChangesAsync();

            return "Sản phẩm đã được thêm vào giỏ hàng.";
        }

        public async Task AddOrUpdateGioHangAsync(int maKH, int sanPhamId, int soLuong)
        {
            var gioHang = await _context.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .FirstOrDefaultAsync(g => g.MaKH == maKH);

            if (gioHang == null)
            {
                gioHang = new GioHang { MaKH = maKH };
                _context.GioHangs.Add(gioHang);
            }

            var chiTietGioHang = gioHang.ChiTietGioHangs
                .FirstOrDefault(ct => ct.MaSP == sanPhamId);

            if (chiTietGioHang != null)
            {
                chiTietGioHang.SoLuong += soLuong;
            }
            else
            {
                chiTietGioHang = new ChiTietGioHang
                {
                    GioHangId = gioHang.Id,
                    MaSP = sanPhamId,
                    SoLuong = soLuong
                };
                _context.ChiTietGioHangs.Add(chiTietGioHang);
            }

            await _context.SaveChangesAsync();
        }
    }
}
