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
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<SanPhamKhuyenMai> SanPhamKhuyenMais { get; set; }
        public DbSet<BinhLuan> BinhLuans { get; set; }

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
            

            modelBuilder.Entity<BinhLuan>()
                .HasOne(bl => bl.KhachHang)
                .WithMany(kh => kh.BinhLuans)
                .HasForeignKey(bl => bl.MaKH);

            modelBuilder.Entity<BinhLuan>()
                .HasOne(bl => bl.SanPham)
                .WithMany(sp => sp.BinhLuans)
                .HasForeignKey(bl => bl.MaSP);


            modelBuilder.Entity<KhuyenMai>()
                .HasMany(km => km.SanPhamModels)
                .WithMany(sp => sp.KhuyenMais)
                .UsingEntity<Dictionary<string, object>>(
                    "SanPhamKhuyenMai",
                    j => j
                        .HasOne<SanPhamModel>()
                        .WithMany()
                        .HasForeignKey("MaSP")
                        .HasConstraintName("FK_SanPhamKhuyenMai_SanPhamModel_MaSP")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<KhuyenMai>()
                        .WithMany()
                        .HasForeignKey("MaKM")
                        .HasConstraintName("FK_SanPhamKhuyenMai_KhuyenMai_MaKM")
                        .OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
