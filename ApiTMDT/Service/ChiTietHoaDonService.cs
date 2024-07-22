using ApiTMDT.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiTMDT.Service
{
    public class ChiTietHoaDonService
    {
            private readonly ApiDbContext _context;

            public ChiTietHoaDonService(ApiDbContext context)
            {
                _context = context;
            }

            public async Task<List<ChiTietHoaDon>> GetAllChiTietHoaDonsAsync(int pageNumber = 1, int pageSize = 10)
            {
                return await _context.ChiTietHoaDons
                    .Include(cthd => cthd.HoaDon)
                    .Include(cthd => cthd.SanPham)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            public async Task<(ChiTietHoaDon chiTietHoaDon, string message)> CreateChiTietHoaDonAsync(ChiTietHoaDon chiTietHoaDon)
            {
                _context.ChiTietHoaDons.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();

                var createdChiTietHoaDon = await _context.ChiTietHoaDons
                    .Include(cthd => cthd.HoaDon)
                    .Include(cthd => cthd.SanPham)
                    .FirstOrDefaultAsync(cthd => cthd.MaCTHD == chiTietHoaDon.MaCTHD);

                return (createdChiTietHoaDon, "Tạo chi tiết hóa đơn thành công.");
            }

            public async Task<(ChiTietHoaDon originalChiTietHoaDon, ChiTietHoaDon updatedChiTietHoaDon, string message)> UpdateChiTietHoaDonAsync(int id, ChiTietHoaDon chiTietHoaDonUpdate)
            {
                var existingChiTietHoaDon = await _context.ChiTietHoaDons.FindAsync(id);
                if (existingChiTietHoaDon == null)
                {
                    return (null, null, "Chi tiết hóa đơn không tồn tại.");
                }

                var originalChiTietHoaDon = new ChiTietHoaDon
                {
                    MaCTHD = existingChiTietHoaDon.MaCTHD,
                    MaHD = existingChiTietHoaDon.MaHD,
                    MaSP = existingChiTietHoaDon.MaSP,
                    SoLuong = existingChiTietHoaDon.SoLuong,
                    DonGia = existingChiTietHoaDon.DonGia
                };

                existingChiTietHoaDon.MaHD = chiTietHoaDonUpdate.MaHD;
                existingChiTietHoaDon.MaSP = chiTietHoaDonUpdate.MaSP;
                existingChiTietHoaDon.SoLuong = chiTietHoaDonUpdate.SoLuong;
                existingChiTietHoaDon.DonGia = chiTietHoaDonUpdate.DonGia;

                _context.ChiTietHoaDons.Update(existingChiTietHoaDon);
                await _context.SaveChangesAsync();

                var updatedChiTietHoaDon = await _context.ChiTietHoaDons
                    .Include(cthd => cthd.HoaDon)
                    .Include(cthd => cthd.SanPham)
                    .FirstOrDefaultAsync(cthd => cthd.MaCTHD == existingChiTietHoaDon.MaCTHD);

                return (originalChiTietHoaDon, updatedChiTietHoaDon, "Cập nhật chi tiết hóa đơn thành công.");
            }

            public async Task<(List<ChiTietHoaDon> chiTietHoaDons, string message)> SearchChiTietHoaDonAsync(string maHDOrMaSP)
            {
                var query = _context.ChiTietHoaDons.AsQueryable();

                if (!string.IsNullOrWhiteSpace(maHDOrMaSP))
                {
                    if (int.TryParse(maHDOrMaSP, out int maHD))
                    {
                        query = query.Where(cthd => cthd.MaHD == maHD);
                    }
                    else if (int.TryParse(maHDOrMaSP, out int maSP))
                    {
                        query = query.Where(cthd => cthd.MaSP == maSP);
                    }
                }

                var chiTietHoaDons = await query.ToListAsync();

                if (!chiTietHoaDons.Any())
                {
                    return (chiTietHoaDons, "Không tìm thấy chi tiết hóa đơn nào khớp với điều kiện tìm kiếm.");
                }

                return (chiTietHoaDons, "Tìm kiếm thành công.");
            }
        }
    }
