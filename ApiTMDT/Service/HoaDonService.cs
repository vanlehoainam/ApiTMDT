using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTMDT.Service
{
    public class HoaDonService
    {
        private readonly ApiDbContext _context;

        public HoaDonService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<HoaDon>> GetAllHoaDonsAsync(int pageNumber = 1, int pageSize = 5)
        {
            return await _context.HoaDons
                .Include(hd => hd.KhachHang)
                .Include(hd => hd.ChiTietHoaDons)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(HoaDon hoaDon, string message)> CreateHoaDonAsync(HoaDon hoaDon)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            var createdHoaDon = await _context.HoaDons
                .Include(hd => hd.KhachHang)
                .Include(hd => hd.ChiTietHoaDons)
                .FirstOrDefaultAsync(hd => hd.MaHD == hoaDon.MaHD);

            return (createdHoaDon, "Tạo hóa đơn thành công.");
        }

        public async Task<(HoaDon originalHoaDon, HoaDon updatedHoaDon, string message)> UpdateHoaDonAsync(int id, HoaDon hoaDonUpdate)
        {
            var existingHoaDon = await _context.HoaDons.FindAsync(id);
            if (existingHoaDon == null)
            {
                return (null, null, "Hóa đơn không tồn tại.");
            }

            var originalHoaDon = new HoaDon
            {
                MaHD = existingHoaDon.MaHD,
                NgayLap = existingHoaDon.NgayLap,
                TongTien = existingHoaDon.TongTien,
                MaKH = existingHoaDon.MaKH
            };

            existingHoaDon.NgayLap = hoaDonUpdate.NgayLap;
            existingHoaDon.TongTien = hoaDonUpdate.TongTien;
            existingHoaDon.MaKH = hoaDonUpdate.MaKH;
            existingHoaDon.TrangThai = hoaDonUpdate.TrangThai;
            existingHoaDon.PhuongThucThanhToan = hoaDonUpdate.PhuongThucThanhToan;
            existingHoaDon.GhiChu = hoaDonUpdate.GhiChu;

            _context.HoaDons.Update(existingHoaDon);
            await _context.SaveChangesAsync();

            var updatedHoaDon = await _context.HoaDons
                .Include(hd => hd.KhachHang)
                .Include(hd => hd.ChiTietHoaDons)
                .FirstOrDefaultAsync(hd => hd.MaHD == existingHoaDon.MaHD);

            return (originalHoaDon, updatedHoaDon, "Cập nhật hóa đơn thành công.");
        }

        public async Task<(List<HoaDon> hoaDons, string message)> SearchHoaDonAsync(string maKHOrNgayLap)
        {
            var query = _context.HoaDons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(maKHOrNgayLap))
            {
                if (int.TryParse(maKHOrNgayLap, out int maKH))
                {
                    query = query.Where(hd => hd.MaKH == maKH);
                }
                else if (DateTime.TryParse(maKHOrNgayLap, out DateTime ngayLap))
                {
                    query = query.Where(hd => hd.NgayLap.Date == ngayLap.Date);
                }
            }

            var hoaDons = await query.ToListAsync();

            if (!hoaDons.Any())
            {
                return (hoaDons, "Không tìm thấy hóa đơn nào khớp với điều kiện tìm kiếm.");
            }

            return (hoaDons, "Tìm kiếm thành công.");
        }

        public async Task<(HoaDon hoaDon, string message)> CreateHoaDonWithDetailsAsync(HoaDon hoaDon, List<ChiTietHoaDon> chiTietHoaDons)
        {
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            foreach (var chiTietHoaDon in chiTietHoaDons)
            {
                chiTietHoaDon.MaHD = hoaDon.MaHD;
                _context.ChiTietHoaDons.Add(chiTietHoaDon);
            }

            await _context.SaveChangesAsync();

            var createdHoaDon = await _context.HoaDons
                .Include(hd => hd.KhachHang)
                .Include(hd => hd.ChiTietHoaDons)
                .FirstOrDefaultAsync(hd => hd.MaHD == hoaDon.MaHD);

            return (createdHoaDon, "Tạo hóa đơn với chi tiết thành công.");
        }

        public async Task<string> AddToHoaDonAsync(int maHD, int maSP, int soLuong)
        {
            var hoaDon = await _context.HoaDons.FirstOrDefaultAsync(hd => hd.MaHD == maHD);
            if (hoaDon == null)
            {
                return "Hóa đơn không tồn tại.";
            }

            var chiTietHoaDon = new ChiTietHoaDon
            {
                MaHD = hoaDon.MaHD,
                MaSP = maSP,
                SoLuong = soLuong
            };
            _context.ChiTietHoaDons.Add(chiTietHoaDon);
            await _context.SaveChangesAsync();

            return "Sản phẩm đã được thêm vào hóa đơn.";
        }

        public async Task AddOrUpdateHoaDonAsync(int maHD, int sanPhamId, int soLuong)
        {
            var hoaDon = await _context.HoaDons
                .Include(hd => hd.ChiTietHoaDons)
                .FirstOrDefaultAsync(hd => hd.MaHD == maHD);

            if (hoaDon == null)
            {
                return;
            }

            var chiTietHoaDon = hoaDon.ChiTietHoaDons
                .FirstOrDefault(ct => ct.MaSP == sanPhamId);

            if (chiTietHoaDon != null)
            {
                chiTietHoaDon.SoLuong += soLuong;
            }
            else
            {
                chiTietHoaDon = new ChiTietHoaDon
                {
                    MaHD = hoaDon.MaHD,
                    MaSP = sanPhamId,
                    SoLuong = soLuong
                };
                _context.ChiTietHoaDons.Add(chiTietHoaDon);
            }

            await _context.SaveChangesAsync();
        }
    }
}
