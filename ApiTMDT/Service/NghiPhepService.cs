using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTMDT.Service
{
    public class NghiPhepService
    {
        private readonly ApiDbContext _context;
        public NghiPhepService (ApiDbContext context)
        {
            _context = context;
        }
        public async Task<List<NghiPhep>> GetAllNghiPhepsAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.NghiPhep
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(NghiPhep nghiPhep, string message)> CreateNghiPhepAsync(NghiPhep nghiPhep)
        {
            _context.NghiPhep.Add(nghiPhep);
            await _context.SaveChangesAsync();
            return (nghiPhep, "Tạo nghỉ phép thành công.");
        }

        public async Task<(NghiPhep originalNghiPhep, NghiPhep updatedNghiPhep, string message)> UpdateNghiPhepAsync(string id, NghiPhep nghiPhepUpdate)
        {
            var existingNghiPhep = await _context.NghiPhep.FindAsync(id);
            if (existingNghiPhep == null)
            {
                return (null, null, "Nghỉ phép không tồn tại.");
            }

            var originalNghiPhep = new NghiPhep
            {
                MaNP = existingNghiPhep.MaNP,
                NgayBatDau = existingNghiPhep.NgayBatDau,
                NgayKetThuc = existingNghiPhep.NgayKetThuc,
                LyDo = existingNghiPhep.LyDo,
                TrangThai = existingNghiPhep.TrangThai,
                MaNV = existingNghiPhep.MaNV
            };

            existingNghiPhep.NgayBatDau = nghiPhepUpdate.NgayBatDau;
            existingNghiPhep.NgayKetThuc = nghiPhepUpdate.NgayKetThuc;
            existingNghiPhep.LyDo = nghiPhepUpdate.LyDo;
            existingNghiPhep.TrangThai = nghiPhepUpdate.TrangThai;
            existingNghiPhep.MaNV = nghiPhepUpdate.MaNV;

            _context.NghiPhep.Update(existingNghiPhep);
            await _context.SaveChangesAsync();

            return (originalNghiPhep, existingNghiPhep, "Cập nhật nghỉ phép thành công.");
        }
    }
}
