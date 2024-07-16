using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTMDT.Service
{
    public class KhuyenMaiService
    {
        private readonly ApiDbContext _context;

        public KhuyenMaiService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhuyenMai>> GetAllKhuyenMaisAsync()
        {
            return await _context.KhuyenMais
                .Include(km => km.SanPhamKhuyenMais)
                .ToListAsync();
        }

        public async Task<KhuyenMai> GetKhuyenMaiByIdAsync(int id)
        {
            return await _context.KhuyenMais
                .Include(km => km.SanPhamKhuyenMais)
                .FirstOrDefaultAsync(km => km.MaKM == id);
        }

        public async Task<(KhuyenMai khuyenMai, string message)> CreateKhuyenMaiAsync(KhuyenMai khuyenMai)
        {
            _context.KhuyenMais.Add(khuyenMai);
            await _context.SaveChangesAsync();

            var createdKhuyenMai = await _context.KhuyenMais
                .Include(km => km.SanPhamKhuyenMais)
                .FirstOrDefaultAsync(km => km.MaKM == khuyenMai.MaKM);

            return (createdKhuyenMai, "Tạo khuyến mãi thành công.");
        }

        public async Task<(KhuyenMai khuyenMai, string message)> UpdateKhuyenMaiAsync(int id, KhuyenMai khuyenMaiUpdate)
        {
            var existingKhuyenMai = await _context.KhuyenMais.FindAsync(id);
            if (existingKhuyenMai == null)
            {
                return (null, "Khuyến mãi không tồn tại.");
            }

            existingKhuyenMai.TenKM = khuyenMaiUpdate.TenKM;
            existingKhuyenMai.MoTa = khuyenMaiUpdate.MoTa;
            existingKhuyenMai.NgayBatDau = khuyenMaiUpdate.NgayBatDau;
            existingKhuyenMai.NgayKetThuc = khuyenMaiUpdate.NgayKetThuc;
            existingKhuyenMai.PhanTramGiam = khuyenMaiUpdate.PhanTramGiam;
            existingKhuyenMai.TrangThai = khuyenMaiUpdate.TrangThai;

            _context.KhuyenMais.Update(existingKhuyenMai);
            await _context.SaveChangesAsync();

            var updatedKhuyenMai = await _context.KhuyenMais
                .Include(km => km.SanPhamKhuyenMais)
                .FirstOrDefaultAsync(km => km.MaKM == existingKhuyenMai.MaKM);

            return (updatedKhuyenMai, "Cập nhật khuyến mãi thành công.");
        }

        public async Task<(bool success, string message)> DeleteKhuyenMaiAsync(int id)
        {
            var khuyenMai = await _context.KhuyenMais.FindAsync(id);
            if (khuyenMai == null)
            {
                return (false, "Khuyến mãi không tồn tại.");
            }

            _context.KhuyenMais.Remove(khuyenMai);
            await _context.SaveChangesAsync();

            return (true, "Xóa khuyến mãi thành công.");
        }
    }
}
