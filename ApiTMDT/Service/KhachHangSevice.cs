using ApiTMDT.Models;
using Data;

using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace ApiTMDT.Service
{
    public class KhachHangSevice
    {
        private readonly ApiDbContext _context;

        public KhachHangSevice(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<KhachHang>> GetAllKhachHangsAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.KhachHangs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(KhachHang khachHang, string message)> CreateKhachHangAsync(KhachHang khachHang)
        {
            var existingKhachHangByName = await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.HoTen == khachHang.HoTen);

            if (existingKhachHangByName != null)
            {
                return (null, "Tên khách hàng đã tồn tại.");
            }

            _context.KhachHangs.Add(khachHang);
            await _context.SaveChangesAsync();

            return (khachHang, "Tạo khách hàng thành công.");
        }

        public async Task<(KhachHang originalKhachHang, KhachHang updatedKhachHang, string message)> UpdateKhachHangAsync(int id, KhachHang khachHangUpdate)
        {
            var existingKhachHang = await _context.KhachHangs.FindAsync(id);
            if (existingKhachHang == null)
            {
                return (null, null, "Khách hàng không tồn tại.");
            }

            var khachHangWithSameName = await _context.KhachHangs
                .FirstOrDefaultAsync(kh => kh.HoTen == khachHangUpdate.HoTen && kh.MaKH != id);

            if (khachHangWithSameName != null)
            {
                return (null, null, "Tên khách hàng đã tồn tại.");
            }

            var originalKhachHang = new KhachHang
            {
                MaKH = existingKhachHang.MaKH,
                HoTen = existingKhachHang.HoTen,
                DiaChi = existingKhachHang.DiaChi,
                SoDienThoai = existingKhachHang.SoDienThoai,
                Email = existingKhachHang.Email
            };

            existingKhachHang.HoTen = khachHangUpdate.HoTen;
            existingKhachHang.DiaChi = khachHangUpdate.DiaChi;
            existingKhachHang.SoDienThoai = khachHangUpdate.SoDienThoai;
            existingKhachHang.Email = khachHangUpdate.Email;

            _context.KhachHangs.Update(existingKhachHang);
            await _context.SaveChangesAsync();

            return (originalKhachHang, existingKhachHang, "Cập nhật khách hàng thành công.");
        }

        public async Task<DeleteKhachHangResponse> DeleteKhachHangAsync(int id)
        {
            var existingKhachHang = await _context.KhachHangs.FindAsync(id);
            if (existingKhachHang == null)
            {
                return new DeleteKhachHangResponse
                {
                    Success = false,
                    Message = "Không tìm thấy khách hàng"
                };
            }

            _context.KhachHangs.Remove(existingKhachHang);
            await _context.SaveChangesAsync();

            return new DeleteKhachHangResponse
            {
                Success = true,
                Message = "Xóa thành công"
            };
        }


        public async Task<(List<string> khachHangsWithOrders, string message)> SearchKhachHangAsync(string nameOrPhone)
        {
            var query = _context.KhachHangs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameOrPhone))
            {
                query = query.Where(kh => kh.HoTen.Contains(nameOrPhone) || kh.SoDienThoai.Contains(nameOrPhone));
            }

            var khachHangs = await query.Include(kh => kh.HoaDons).ToListAsync();

            if (!khachHangs.Any())
            {
                return (new List<string>(), "Không tìm thấy khách hàng nào khớp với điều kiện tìm kiếm.");
            }

            var khachHangsWithOrders = new List<string>();
            foreach (var khachHang in khachHangs)
            {
                var ordersInfo = new StringBuilder();
                ordersInfo.AppendLine($"Khách hàng: {khachHang.HoTen}, Số điện thoại: {khachHang.SoDienThoai}");
                foreach (var hoaDon in khachHang.HoaDons)
                {
                    ordersInfo.AppendLine($"  Hóa đơn ID: {hoaDon.MaHD}, Ngày lập: {hoaDon.NgayLap}");
                }
                khachHangsWithOrders.Add(ordersInfo.ToString());
            }

            return (khachHangsWithOrders, "Tìm kiếm thành công.");
        }

        public class DeleteKhachHangResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}

