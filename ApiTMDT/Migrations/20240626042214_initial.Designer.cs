﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiTMDT.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240626042214_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiTMDT.Models.HopDongLaoDong", b =>
                {
                    b.Property<int>("MaHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHD"));

                    b.Property<DateTime>("DenNgay")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoaiHD")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("MaNV")
                        .HasColumnType("int");

                    b.Property<DateTime>("TuNgay")
                        .HasColumnType("datetime2");

                    b.HasKey("MaHD");

                    b.HasIndex("MaNV");

                    b.ToTable("HopDongLaoDong");
                });

            modelBuilder.Entity("ApiTMDT.Models.NhanVien", b =>
                {
                    b.Property<int>("MaNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNV"));

                    b.Property<int>("CCCD")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GioiTinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Luong")
                        .HasColumnType("int");

                    b.Property<int?>("MaHD")
                        .HasColumnType("int");

                    b.Property<int?>("MaPB")
                        .HasColumnType("int");

                    b.Property<int?>("MaTDHV")
                        .HasColumnType("int");

                    b.Property<string>("NgaySinh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QueQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNV");

                    b.HasIndex("MaHD");

                    b.HasIndex("MaPB");

                    b.HasIndex("MaTDHV");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("ApiTMDT.Models.PhongBan", b =>
                {
                    b.Property<int>("MaPB")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaPB"));

                    b.Property<int?>("MaTP")
                        .HasColumnType("int");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenPB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaPB");

                    b.HasIndex("MaTP");

                    b.ToTable("PhongBan");
                });

            modelBuilder.Entity("ApiTMDT.Models.SanPhamModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Anh_SP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("Ten_SP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("ApiTMDT.Models.TrinhDoHocVan", b =>
                {
                    b.Property<int>("MaTDHV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaTDHV"));

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenTDHV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenTDNN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaTDHV");

                    b.ToTable("TrinhDoHocVan");
                });

            modelBuilder.Entity("ApiTMDT.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
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
                    b.HasOne("ApiTMDT.Models.HopDongLaoDong", "HopDongLaoDong")
                        .WithMany()
                        .HasForeignKey("MaHD");

                    b.HasOne("ApiTMDT.Models.PhongBan", "PhongBan")
                        .WithMany()
                        .HasForeignKey("MaPB");

                    b.HasOne("ApiTMDT.Models.TrinhDoHocVan", "TrinhDoHocVan")
                        .WithMany()
                        .HasForeignKey("MaTDHV");

                    b.Navigation("HopDongLaoDong");

                    b.Navigation("PhongBan");

                    b.Navigation("TrinhDoHocVan");
                });

            modelBuilder.Entity("ApiTMDT.Models.PhongBan", b =>
                {
                    b.HasOne("ApiTMDT.Models.NhanVien", "TruongPhong")
                        .WithMany()
                        .HasForeignKey("MaTP");

                    b.Navigation("TruongPhong");
                });
#pragma warning restore 612, 618
        }
    }
}
