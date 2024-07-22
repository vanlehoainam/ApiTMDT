using ApiTMDT.Models;
using Data;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ApiTMDT.Service
{
    public class PhongBanService
    {
        private readonly ApiDbContext _context;

        public PhongBanService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<PhongBan>> GetAllPhongBansAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.PhongBan
                
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(PhongBan phongBan, string message)> CreatePhongBanAsync(PhongBan phongBan)
        {
            var existingPhongBanByName = await _context.PhongBan
                .FirstOrDefaultAsync(pb => pb.TenPB == phongBan.TenPB);

            if (existingPhongBanByName != null)
            {
                return (null, "Tên phòng ban đã tồn tại.");
            }

            _context.PhongBan.Add(phongBan);
            await _context.SaveChangesAsync();

            return (phongBan, "Tạo phòng ban thành công.");
        }

        public async Task<(PhongBan originalPhongBan, PhongBan updatedPhongBan, string message)> UpdatePhongBanAsync(int id, PhongBan phongBanUpdate)
        {
            var existingPhongBan = await _context.PhongBan.FindAsync(id);
            if (existingPhongBan == null)
            {
                return (null, null, "Phòng ban không tồn tại.");
            }

            var phongBanWithSameName = await _context.PhongBan
                .FirstOrDefaultAsync(pb => pb.TenPB == phongBanUpdate.TenPB && pb.MaPB != id);

            if (phongBanWithSameName != null)
            {
                return (null, null, "Tên phòng ban đã tồn tại.");
            }

            var originalPhongBan = new PhongBan
            {
                MaPB = existingPhongBan.MaPB,
                TenPB = existingPhongBan.TenPB,
                SDT = existingPhongBan.SDT,
               
            };

            existingPhongBan.TenPB = phongBanUpdate.TenPB;
            existingPhongBan.SDT = phongBanUpdate.SDT;
          

            _context.PhongBan.Update(existingPhongBan);
            await _context.SaveChangesAsync();

            return (originalPhongBan, existingPhongBan, "Cập nhật phòng ban thành công.");
        }

       
        public async Task<(List<PhongBan> phongBans, string message)> SearchPhongBanAsync(string nameOrPhone)
        {
            var query = _context.PhongBan.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameOrPhone))
            {
                query = query.Where(pb => pb.TenPB.Contains(nameOrPhone) || pb.SDT.Contains(nameOrPhone));
            }

            var phongBans = await query.ToListAsync();

            if (!phongBans.Any())
            {
                return (phongBans, "Không tìm thấy phòng ban nào khớp với điều kiện tìm kiếm.");
            }

            return (phongBans, "Tìm kiếm thành công.");
        }

        public class DeletePhongBanResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
