using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTMDT.Service
{
    public class BinhLuanService
    {
        private readonly ApiDbContext _context;

        public BinhLuanService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<BinhLuan>> GetAllBinhLuansAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.BinhLuans
                .Include(bl => bl.KhachHang)
                .Include(bl => bl.SanPham)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(BinhLuan binhLuan, string message)> CreateBinhLuanAsync(BinhLuan binhLuan)
        {
            _context.BinhLuans.Add(binhLuan);
            await _context.SaveChangesAsync();

            var createdBinhLuan = await _context.BinhLuans
                .Include(bl => bl.KhachHang)
                .Include(bl => bl.SanPham)
                .FirstOrDefaultAsync(bl => bl.MaBL == binhLuan.MaBL);

            if (createdBinhLuan == null)
            {
                return (null, "Tạo bình luận không thành công.");
            }

            return (createdBinhLuan, "Tạo bình luận thành công.");
        }

        public async Task<(BinhLuan binhLuan, string message)> UpdateBinhLuanAsync(int id, BinhLuan binhLuanUpdate)
        {
            var existingBinhLuan = await _context.BinhLuans.FindAsync(id);
            if (existingBinhLuan == null)
            {
                return (null, "Bình luận không tồn tại.");
            }

            existingBinhLuan.NoiDung = binhLuanUpdate.NoiDung;
            existingBinhLuan.DiemDanhGia = binhLuanUpdate.DiemDanhGia;
            existingBinhLuan.NgayDang = binhLuanUpdate.NgayDang;
            existingBinhLuan.MaKH = binhLuanUpdate.MaKH;
            existingBinhLuan.MaSP = binhLuanUpdate.MaSP;

            _context.BinhLuans.Update(existingBinhLuan);
            await _context.SaveChangesAsync();

            var updatedBinhLuan = await _context.BinhLuans
                .Include(bl => bl.KhachHang)
                .Include(bl => bl.SanPham)
                .FirstOrDefaultAsync(bl => bl.MaBL == existingBinhLuan.MaBL);

            if (updatedBinhLuan == null)
            {
                return (null, "Cập nhật bình luận không thành công.");
            }

            return (updatedBinhLuan, "Cập nhật bình luận thành công.");
        }

        public async Task<(bool success, string message)> DeleteBinhLuanAsync(int id)
        {
            var binhLuan = await _context.BinhLuans.FindAsync(id);
            if (binhLuan == null)
            {
                return (false, "Bình luận không tồn tại.");
            }

            _context.BinhLuans.Remove(binhLuan);
            await _context.SaveChangesAsync();

            return (true, "Xóa bình luận thành công.");
        }
    }
}
