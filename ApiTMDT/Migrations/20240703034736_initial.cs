using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTMDT.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NghiPhep",
                columns: table => new
                {
                    MaNP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    MaNV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NghiPhep", x => x.MaNP);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_SP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Anh_SP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrinhDoHocVan",
                columns: table => new
                {
                    MaTDHV = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTDHV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenTDNN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrinhDoHocVan", x => x.MaTDHV);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HopDongLaoDong",
                columns: table => new
                {
                    MaHD = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    LoaiHD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TuNgay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DenNgay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongLaoDong", x => x.MaHD);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Luong = table.Column<int>(type: "int", nullable: false),
                    MaTDHV = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaPB = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaHD = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MaNP = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                    table.ForeignKey(
                        name: "FK_NhanVien_HopDongLaoDong_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HopDongLaoDong",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "FK_NhanVien_NghiPhep_MaNP",
                        column: x => x.MaNP,
                        principalTable: "NghiPhep",
                        principalColumn: "MaNP");
                    table.ForeignKey(
                        name: "FK_NhanVien_TrinhDoHocVan_MaTDHV",
                        column: x => x.MaTDHV,
                        principalTable: "TrinhDoHocVan",
                        principalColumn: "MaTDHV");
                });

            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    MaPB = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenPB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaTP = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.MaPB);
                    table.ForeignKey(
                        name: "FK_PhongBan_NhanVien_MaTP",
                        column: x => x.MaTP,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HopDongLaoDong_MaNV",
                table: "HopDongLaoDong",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaHD",
                table: "NhanVien",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaNP",
                table: "NhanVien",
                column: "MaNP");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaPB",
                table: "NhanVien",
                column: "MaPB");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_MaTDHV",
                table: "NhanVien",
                column: "MaTDHV");

            migrationBuilder.CreateIndex(
                name: "IX_PhongBan_MaTP",
                table: "PhongBan",
                column: "MaTP");

            migrationBuilder.AddForeignKey(
                name: "FK_HopDongLaoDong_NhanVien_MaNV",
                table: "HopDongLaoDong",
                column: "MaNV",
                principalTable: "NhanVien",
                principalColumn: "MaNV",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhanVien_PhongBan_MaPB",
                table: "NhanVien",
                column: "MaPB",
                principalTable: "PhongBan",
                principalColumn: "MaPB");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDongLaoDong_NhanVien_MaNV",
                table: "HopDongLaoDong");

            migrationBuilder.DropForeignKey(
                name: "FK_PhongBan_NhanVien_MaTP",
                table: "PhongBan");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "HopDongLaoDong");

            migrationBuilder.DropTable(
                name: "NghiPhep");

            migrationBuilder.DropTable(
                name: "PhongBan");

            migrationBuilder.DropTable(
                name: "TrinhDoHocVan");
        }
    }
}
