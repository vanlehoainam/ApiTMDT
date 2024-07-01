using ApiTMDT.Models;
using Data;
using System.Data.Entity;

namespace ApiTMDT.Service
{
    public class HocVanService
    {
        private readonly ApiDbContext _context;

        public HocVanService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<TrinhDoHocVan>> GetAllHocVansAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.TrinhDoHocVan
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(TrinhDoHocVan hocVan, string message)> CreateHocVanAsync(TrinhDoHocVan hocVan)
        {
            _context.TrinhDoHocVan.Add(hocVan);
            await _context.SaveChangesAsync();
            return (hocVan, "Tạo học vấn thành công.");
        }

        public async Task<(TrinhDoHocVan originalHocVan, TrinhDoHocVan updatedHocVan, string message)> UpdateHocVanAsync(int id, TrinhDoHocVan hocVanUpdate)
        {
            var existingHocVan = await _context.TrinhDoHocVan.FindAsync(id);
            if (existingHocVan == null)
            {
                return (null, null, "Học vấn không tồn tại.");
            }

            var originalHocVan = new TrinhDoHocVan
            {
                MaTDHV = existingHocVan.MaTDHV,
                TenTDHV = existingHocVan.TenTDHV
            };

            existingHocVan.TenTDHV = hocVanUpdate.TenTDHV;

            _context.TrinhDoHocVan.Update(existingHocVan);
            await _context.SaveChangesAsync();

            return (originalHocVan, existingHocVan, "Cập nhật học vấn thành công.");
        }

        public async Task<DeleteResponse> DeleteHocVanAsync(int id)
        {
            var existingHocVan = await _context.TrinhDoHocVan.FindAsync(id);
            if (existingHocVan == null)
            {
                return new DeleteResponse
                {
                    Success = false,
                    Message = "Không tìm thấy học vấn"
                };
            }

            _context.TrinhDoHocVan.Remove(existingHocVan);
            await _context.SaveChangesAsync();

            return new DeleteResponse
            {
                Success = true,
                Message = "Xóa thành công"
            };
        }

        public class DeleteResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}
