using ApiTMDT.Models;
using Data;

using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ApiTMDT.Service
{
    public class NhanVienSevice
    {

        private readonly ApiDbContext _context;

        public NhanVienSevice(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<NhanVien>> GetAllNhanViensAsync(int pageNumber = 1, int pageSize = 10)
        {
            return await _context.NhanVien
               .Include(nv => nv.TrinhDoHocVan)
                .Include(nv => nv.PhongBan)               
               
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

            public async Task<(NhanVien nhanVien, string message)> CreateNhanVienAsync(NhanVien nhanVien)
            {
                var existingNhanVienByName = await _context.NhanVien
                    .FirstOrDefaultAsync(nv => nv.HoTen == nhanVien.HoTen);

                if (existingNhanVienByName != null)
                {
                    return (null, "Tên nhân viên đã tồn tại.");
                }
                             

                _context.NhanVien.Add(nhanVien);
                await _context.SaveChangesAsync();

                return (nhanVien, "Tạo nhân viên thành công.");
            }

        public async Task<(NhanVien originalNhanVien, NhanVien updatedNhanVien, string message)> UpdateNhanVienAsync(int id, NhanVien nhanVienUpdate)
        {
            var existingNhanVien = await _context.NhanVien.FindAsync(id);
            if (existingNhanVien == null)
            {
                return (null, null, "Nhân viên không tồn tại.");
            }

            var nhanVienWithSameName = await _context.NhanVien
                .FirstOrDefaultAsync(nv => nv.HoTen == nhanVienUpdate.HoTen && nv.MaNV != id);

            if (nhanVienWithSameName != null)
            {
                return (null, null, "Tên nhân viên đã tồn tại.");
            }

            var originalNhanVien = new NhanVien
            {
                MaNV = existingNhanVien.MaNV,
                HoTen = existingNhanVien.HoTen,
                DiaChi = existingNhanVien.DiaChi,
                CCCD = existingNhanVien.CCCD,
                GioiTinh = existingNhanVien.GioiTinh,
                QueQuan = existingNhanVien.QueQuan,
                NgaySinh = existingNhanVien.NgaySinh,
                SoDienThoai = existingNhanVien.SoDienThoai,
                Email = existingNhanVien.Email,
                Luong = existingNhanVien.Luong,
                MaTDHV = existingNhanVien.MaTDHV,
                MaPB = existingNhanVien.MaPB,
                
            };

            existingNhanVien.HoTen = nhanVienUpdate.HoTen;
            existingNhanVien.DiaChi = nhanVienUpdate.DiaChi;
            existingNhanVien.CCCD = nhanVienUpdate.CCCD;
            existingNhanVien.GioiTinh = nhanVienUpdate.GioiTinh;
            existingNhanVien.QueQuan = nhanVienUpdate.QueQuan;
            existingNhanVien.NgaySinh = nhanVienUpdate.NgaySinh;
            existingNhanVien.SoDienThoai = nhanVienUpdate.SoDienThoai;
            existingNhanVien.Email = nhanVienUpdate.Email;
            existingNhanVien.Luong = nhanVienUpdate.Luong;
            existingNhanVien.MaTDHV = nhanVienUpdate.MaTDHV;
            existingNhanVien.MaPB = nhanVienUpdate.MaPB;
       

            _context.NhanVien.Update(existingNhanVien);
            await _context.SaveChangesAsync();

            return (originalNhanVien, existingNhanVien, "Cập nhật nhân viên thành công.");
        }

        public async Task<DeleteNhanVienResponse> DeleteNhanVienAsync(int id)
        {
            var existingNhanVien = await _context.NhanVien.FindAsync(id);
            if (existingNhanVien == null)
            {
                return new DeleteNhanVienResponse
                {
                    Success = false,
                    Message = "Không tìm thấy nhân viên"
                };
            }

            _context.NhanVien.Remove(existingNhanVien);
            await _context.SaveChangesAsync();

            return new DeleteNhanVienResponse
            {
                Success = true,
                Message = "Xóa thành công"
            };
        }

        public async Task<(List<NhanVien> nhanViens, string message)> SearchNhanVienAsync(string nameOrCCCD)
        {
            var query = _context.NhanVien.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameOrCCCD))
            {
                if (int.TryParse(nameOrCCCD, out int cccd))
                {
                    query = query.Where(nv => nv.CCCD == cccd);
                }
                else
                {
                    query = query.Where(nv => nv.HoTen.Contains(nameOrCCCD));
                }
            }

            var nhanViens = await query.ToListAsync();

            if (!nhanViens.Any())
            {
                return (nhanViens, "Không tìm thấy nhân viên nào khớp với điều kiện tìm kiếm.");
            }

            return (nhanViens, "Tìm kiếm thành công.");
        }

        public class DeleteNhanVienResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }

}
