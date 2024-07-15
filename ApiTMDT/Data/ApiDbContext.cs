using ApiTMDT.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext( DbContextOptions<ApiDbContext> opt) : base(opt) 
        { 
        }
        public DbSet <UserModel> Users { get; set; }
        public DbSet<SanPhamModel> SanPham { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<PhongBan> PhongBan { get; set; }
        public DbSet<TrinhDoHocVan> TrinhDoHocVan { get; set; }
        public DbSet<HopDongLaoDong> HopDongLaoDong { get; set; }
        public DbSet<NghiPhep> NghiPhep { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HoaDon>()
                .HasMany(h => h.ChiTietHoaDons)
                .WithOne(ct => ct.HoaDon)
                .HasForeignKey(ct => ct.MaHD);
            modelBuilder.Entity<GioHang>()
               .HasMany(g => g.ChiTietGioHangs)
               .WithOne(ct => ct.GioHang)
               .HasForeignKey(ct => ct.GioHangId);
        }

    }
}
