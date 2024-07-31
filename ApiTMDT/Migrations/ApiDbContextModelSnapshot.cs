﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiTMDT.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApiTMDT.Models.BinhLuan", b =>
                {
                    b.Property<int>("MaBL")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaBL"), 1L, 1);

                    b.Property<int>("DiemDanhGia")
                        .HasColumnType("int");

                    b.Property<int>("MaKH")
                        .HasColumnType("int");

                    b.Property<int>("MaSP")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayDang")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("MaBL");

                    b.HasIndex("MaKH");

                    b.HasIndex("MaSP");

                    b.ToTable("BinhLuans");
                });

            modelBuilder.Entity("ApiTMDT.Models.ChiTietGioHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GioHangId")
                        .HasColumnType("int");

                    b.Property<int>("MaSP")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GioHangId");

                    b.HasIndex("MaSP");

                    b.ToTable("ChiTietGioHangs");
                });

            modelBuilder.Entity("ApiTMDT.Models.ChiTietHoaDon", b =>
                {
                    b.Property<int>("MaCTHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaCTHD"), 1L, 1);

                    b.Property<decimal>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaHD")
                        .HasColumnType("int");

                    b.Property<int>("MaSP")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaCTHD");

                    b.HasIndex("MaHD");

                    b.HasIndex("MaSP");

                    b.ToTable("ChiTietHoaDons");
                });

            modelBuilder.Entity("ApiTMDT.Models.GioHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaKH")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaKH");

                    b.ToTable("GioHangs");
                });

            modelBuilder.Entity("ApiTMDT.Models.HoaDon", b =>
                {
                    b.Property<int>("MaHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHD"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaKH")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhuongThucThanhToan")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaHD");

                    b.HasIndex("MaKH");

                    b.ToTable("HoaDons");
                });

            modelBuilder.Entity("ApiTMDT.Models.HopDongLaoDong", b =>
                {
                    b.Property<int>("MaHDLD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHDLD"), 1L, 1);

                    b.Property<DateTime>("DenNgay")
                        .HasColumnType("datetime2");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("LoaiHD")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("LuongCoBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MaNV")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayKy")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("TuNgay")
                        .HasColumnType("datetime2");

                    b.HasKey("MaHDLD");

                    b.HasIndex("MaNV");

                    b.ToTable("HopDongLaoDong");
                });

            modelBuilder.Entity("ApiTMDT.Models.KhachHang", b =>
                {
                    b.Property<int>("MaKH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKH"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaKH");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("ApiTMDT.Models.KhuyenMai", b =>
                {
                    b.Property<int>("MaKM")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKM"), 1L, 1);

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PhanTramGiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenKM")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaKM");

                    b.ToTable("KhuyenMais");
                });

            modelBuilder.Entity("ApiTMDT.Models.NghiPhep", b =>
                {
                    b.Property<int>("MaNP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNP"), 1L, 1);

                    b.Property<string>("LyDo")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaNV")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("MaNP");

                    b.ToTable("NghiPhep");
                });

            modelBuilder.Entity("ApiTMDT.Models.NhanVien", b =>
                {
                    b.Property<int>("MaNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNV"), 1L, 1);

                    b.Property<int>("CCCD")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Luong")
                        .HasColumnType("int");

                    b.Property<int?>("MaPB")
                        .HasColumnType("int");

                    b.Property<int?>("MaTDHV")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("MaNV");

                    b.HasIndex("MaPB");

                    b.HasIndex("MaTDHV");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("ApiTMDT.Models.PhongBan", b =>
                {
                    b.Property<int>("MaPB")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPB"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("NgayThanhLap")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenPB")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaPB");

                    b.ToTable("PhongBan");
                });

            modelBuilder.Entity("ApiTMDT.Models.SanPhamModel", b =>
                {
                    b.Property<int>("MaSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaSP"), 1L, 1);

                    b.Property<string>("Anh_SP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("Ten_SP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSP");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("ApiTMDT.Models.TrinhDoHocVan", b =>
                {
                    b.Property<int>("MaTDHV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTDHV"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TenTDHV")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TenTDNN")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MaTDHV");

                    b.ToTable("TrinhDoHocVan");
                });

            modelBuilder.Entity("ApiTMDT.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SanPhamKhuyenMai", b =>
                {
                    b.Property<int>("MaKM")
                        .HasColumnType("int");

                    b.Property<int>("MaSP")
                        .HasColumnType("int");

                    b.HasKey("MaKM", "MaSP");

                    b.HasIndex("MaSP");

                    b.ToTable("SanPhamKhuyenMai");
                });

            modelBuilder.Entity("ApiTMDT.Models.BinhLuan", b =>
                {
                    b.HasOne("ApiTMDT.Models.KhachHang", "KhachHang")
                        .WithMany("BinhLuans")
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTMDT.Models.SanPhamModel", "SanPham")
                        .WithMany("BinhLuans")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ApiTMDT.Models.ChiTietGioHang", b =>
                {
                    b.HasOne("ApiTMDT.Models.GioHang", "GioHang")
                        .WithMany("ChiTietGioHangs")
                        .HasForeignKey("GioHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTMDT.Models.SanPhamModel", "SanPham")
                        .WithMany("ChiTietGioHang")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GioHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ApiTMDT.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("ApiTMDT.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("MaHD")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiTMDT.Models.SanPhamModel", "SanPham")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("ApiTMDT.Models.GioHang", b =>
                {
                    b.HasOne("ApiTMDT.Models.KhachHang", "KhachHang")
                        .WithMany("GioHangs")
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("ApiTMDT.Models.HoaDon", b =>
                {
                    b.HasOne("ApiTMDT.Models.KhachHang", "KhachHang")
                        .WithMany("HoaDons")
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("ApiTMDT.Models.HopDongLaoDong", b =>
                {
                    b.HasOne("ApiTMDT.Models.NhanVien", "NhanVien")
                        .WithMany()
                        .HasForeignKey("MaNV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("ApiTMDT.Models.NhanVien", b =>
                {
                    b.HasOne("ApiTMDT.Models.PhongBan", "PhongBan")
                        .WithMany()
                        .HasForeignKey("MaPB");

                    b.HasOne("ApiTMDT.Models.TrinhDoHocVan", "TrinhDoHocVan")
                        .WithMany()
                        .HasForeignKey("MaTDHV");

                    b.Navigation("PhongBan");

                    b.Navigation("TrinhDoHocVan");
                });

            modelBuilder.Entity("SanPhamKhuyenMai", b =>
                {
                    b.HasOne("ApiTMDT.Models.KhuyenMai", null)
                        .WithMany()
                        .HasForeignKey("MaKM")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_SanPhamKhuyenMai_KhuyenMai_MaKM");

                    b.HasOne("ApiTMDT.Models.SanPhamModel", null)
                        .WithMany()
                        .HasForeignKey("MaSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_SanPhamKhuyenMai_SanPhamModel_MaSP");
                });

            modelBuilder.Entity("ApiTMDT.Models.GioHang", b =>
                {
                    b.Navigation("ChiTietGioHangs");
                });

            modelBuilder.Entity("ApiTMDT.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("ApiTMDT.Models.KhachHang", b =>
                {
                    b.Navigation("BinhLuans");

                    b.Navigation("GioHangs");

                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("ApiTMDT.Models.SanPhamModel", b =>
                {
                    b.Navigation("BinhLuans");

                    b.Navigation("ChiTietGioHang");

                    b.Navigation("ChiTietHoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
