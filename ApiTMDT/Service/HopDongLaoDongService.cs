using ApiTMDT.Models;
using Data;
using System.Data.Entity;

namespace ApiTMDT.Service
{
    public class HopDongLaoDongService
    {
        private readonly ApiDbContext _context;

        public HopDongLaoDongService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<HopDongLaoDong>> GetAllHopDongLaoDongsAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.HopDongLaoDong
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(HopDongLaoDong hopDongLaoDong, string message)> CreateHopDongLaoDongAsync(HopDongLaoDong hopDongLaoDong)
        {
            _context.HopDongLaoDong.Add(hopDongLaoDong);
            await _context.SaveChangesAsync();
            return (hopDongLaoDong, "Tạo hợp đồng lao động thành công.");
        }

        public async Task<(HopDongLaoDong originalHopDongLaoDong, HopDongLaoDong updatedHopDongLaoDong, string message)> UpdateHopDongLaoDongAsync(int id, HopDongLaoDong hopDongLaoDongUpdate)
        {
            var existingHopDongLaoDong = await _context.HopDongLaoDong.FindAsync(id);
            if (existingHopDongLaoDong == null)
            {
                return (null, null, "Hợp đồng lao động không tồn tại.");
            }

            var originalHopDongLaoDong = new HopDongLaoDong
            {
                MaHD = existingHopDongLaoDong.MaHD,
                LoaiHD = existingHopDongLaoDong.LoaiHD,
                TuNgay = existingHopDongLaoDong.TuNgay,
                DenNgay = existingHopDongLaoDong.DenNgay,
                MaNV = existingHopDongLaoDong.MaNV
            };

            existingHopDongLaoDong.LoaiHD = hopDongLaoDongUpdate.LoaiHD;
            existingHopDongLaoDong.TuNgay = hopDongLaoDongUpdate.TuNgay;
            existingHopDongLaoDong.DenNgay = hopDongLaoDongUpdate.DenNgay;
            existingHopDongLaoDong.MaNV = hopDongLaoDongUpdate.MaNV;

            _context.HopDongLaoDong.Update(existingHopDongLaoDong);
            await _context.SaveChangesAsync();

            return (originalHopDongLaoDong, existingHopDongLaoDong, "Cập nhật hợp đồng lao động thành công.");
        }

        public async Task<DeleteResponse> DeleteHopDongLaoDongAsync(int id)
        {
            var existingHopDongLaoDong = await _context.HopDongLaoDong.FindAsync(id);
            if (existingHopDongLaoDong == null)
            {
                return new DeleteResponse
                {
                    Success = false,
                    Message = "Không tìm thấy hợp đồng lao động"
                };
            }

            _context.HopDongLaoDong.Remove(existingHopDongLaoDong);
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
