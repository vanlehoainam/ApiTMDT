﻿using ApiTMDT.Models;
using Data;

using ApiTMDT.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ApiTMDT.Service
{
    public class SanPhamService
    {
        private readonly ApiDbContext _context;

        public SanPhamService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<List<SanPhamModel>> GetAllSanPhamsAsync(/*int pageNumber = 1, int pageSize = 10*/)
        {
            return await _context.SanPham
                /*.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)*/
                .ToListAsync();
        }

        public async Task<(SanPhamModel sanPham, string message)> CreateSanPhamAsync(SanPhamModel sanPham, IFormFile imageFile)
        {
          
            var existingSanPhamByName = await _context.SanPham
                .FirstOrDefaultAsync(sp => sp.Ten_SP == sanPham.Ten_SP);

            if (existingSanPhamByName != null)
            {
                return (null, "Tên sản phẩm đã tồn tại.");
            }

            if (imageFile == null && string.IsNullOrEmpty(sanPham.Anh_SP))
            {
                return (null, "Bạn phải nhập ảnh sản phẩm hoặc chọn tệp ảnh.");
            }

            if (imageFile != null)
            {
                if (imageFile.Length > 0)
                {
                    var fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                    if (fileExtension != ".jpg" && fileExtension != ".jpeg")
                    {
                        return (null, "File ảnh phải có định dạng JPG.");
                    }

                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine("Data", "images", fileName);

                    try
                    {

                        var directoryPath = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }

                        
                        sanPham.Anh_SP = $"/{fileName}";
                    }
                    catch (IOException ioEx)
                    {
                        return (null, $"Lỗi khi lưu ảnh: {ioEx.Message}");
                    }
                    catch (UnauthorizedAccessException uaEx)
                    {
                        return (null, $"Quyền truy cập bị từ chối: {uaEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        return (null, $"Lỗi không xác định: {ex.Message}");
                    }
                }
                else
                {
                    return (null, "File ảnh không hợp lệ.");
                }
            }


            _context.SanPham.Add(sanPham);
            await _context.SaveChangesAsync();

            return (sanPham, "Sản phẩm đã được tạo thành công.");
        }

        public async Task<(SanPhamModel originalSanPham, SanPhamModel updatedSanPham, string message)> UpdateSanPhamAsync(int MaSP, SanPhamModel sanPhamUpdate)
        {
            var existingSanPham = await _context.SanPham.FindAsync(MaSP);
            if (existingSanPham == null)
            {
                return (null, null, "Sản phẩm không tồn tại.");
            }

            var sanPhamWithSameName = await _context.SanPham
                .FirstOrDefaultAsync(sp => sp.Ten_SP == sanPhamUpdate.Ten_SP && sp.MaSP != MaSP);

            if (sanPhamWithSameName != null)
            {
                return (null, null, "Tên sản phẩm đã tồn tại.");
            }

            var originalSanPham = new SanPhamModel
            {
                MaSP = existingSanPham.MaSP,
                Ten_SP = existingSanPham.Ten_SP,
                GhiChu = existingSanPham.GhiChu,
                Gia = existingSanPham.Gia
            };

            existingSanPham.Ten_SP = sanPhamUpdate.Ten_SP;
            existingSanPham.GhiChu = sanPhamUpdate.GhiChu;
            existingSanPham.Gia = sanPhamUpdate.Gia;

            _context.SanPham.Update(existingSanPham);
            await _context.SaveChangesAsync();

            return (originalSanPham, existingSanPham, "Cập nhật sản phẩm thành công.");
        }

        public async Task<DeleteSanPhamResponse> DeleteSanPhamAsync(int id)
        {
            var existingSanPham = await _context.SanPham.FindAsync(id);
            if (existingSanPham == null)
            {
                return new DeleteSanPhamResponse
                {
                    Success = false,
                    Message = "Không tìm thấy sản phẩm"
                };
            }

            _context.SanPham.Remove(existingSanPham);
            await _context.SaveChangesAsync();

            return new DeleteSanPhamResponse
            {
                Success = true,
                Message = "Xóa thành công"
            };
        }

        public async Task<(List<SanPhamModel> sanPhams, string message)> SearchSanPhamAsync(string nameorPrice)
        {
            var query = _context.SanPham.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nameorPrice))
            {
                if (decimal.TryParse(nameorPrice, out decimal price))
                {
                    query = query.Where(sp => sp.Gia == price);
                }
                else
                {
                    query = query.Where(sp => sp.Ten_SP.Contains(nameorPrice));
                }
            }

            var sanPhams = await query.ToListAsync();

            if (!sanPhams.Any())
            {
                return (sanPhams, "Không tìm thấy sản phẩm nào khớp với điều kiện tìm kiếm.");
            }

            return (sanPhams, "Tìm kiếm thành công.");
        }



        public class DeleteSanPhamResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }
}