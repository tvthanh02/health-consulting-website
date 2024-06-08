using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace health_consulting_website.Migrations
{
    /// <inheritdoc />
    public partial class v0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BangThoiGianTuVan",
                columns: table => new
                {
                    sMaThoiGianTuVan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tThoiGianBatDau = table.Column<TimeOnly>(type: "time", nullable: true),
                    tThoiGianKetThuc = table.Column<TimeOnly>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BangThoi__3FCAB332587359FD", x => x.sMaThoiGianTuVan);
                });

            migrationBuilder.CreateTable(
                name: "ChuyenKhoa",
                columns: table => new
                {
                    sMaChuyenKhoa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenChuyenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sMota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sthumbnail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChuyenKh__45D83D3432358F3B", x => x.sMaChuyenKhoa);
                });

            migrationBuilder.CreateTable(
                name: "DonViCongTac",
                columns: table => new
                {
                    sMaDonVi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenDonVi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DonViCon__17898EE8908AF687", x => x.sMaDonVi);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    sMaQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenQuyen = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quyen__0A7BEA5FD9F2B2CC", x => x.sMaQuyen);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    sSessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    sSessionData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dExpireTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Session__138F9F35E2658B1B", x => x.sSessionId);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiTuVan",
                columns: table => new
                {
                    sMaTrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenTrangThai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sMoTaTrangThai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TrangTha__C99ADF4E323444D8", x => x.sMaTrangThai);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    sMaTaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sMatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    sMaQuyen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaiKhoan__0387828823E60E44", x => x.sMaTaiKhoan);
                    table.ForeignKey(
                        name: "FK__TaiKhoan__sMaQuy__1273C1CD",
                        column: x => x.sMaQuyen,
                        principalTable: "Quyen",
                        principalColumn: "sMaQuyen");
                });

            migrationBuilder.CreateTable(
                name: "BacSi",
                columns: table => new
                {
                    sMaBacSi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sTenBacSi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    sAvatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    intNamSinh = table.Column<int>(type: "int", nullable: true),
                    smMoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sMaChuyenKhoa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sMaTaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sMaDonViCongTac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    fPriceConsult = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BacSi__E470D536E8EE2067", x => x.sMaBacSi);
                    table.ForeignKey(
                        name: "FK__BacSi__sMaChuyen__1BFD2C07",
                        column: x => x.sMaChuyenKhoa,
                        principalTable: "ChuyenKhoa",
                        principalColumn: "sMaChuyenKhoa");
                    table.ForeignKey(
                        name: "FK__BacSi__sMaDonViC__1DE57479",
                        column: x => x.sMaDonViCongTac,
                        principalTable: "DonViCongTac",
                        principalColumn: "sMaDonVi");
                    table.ForeignKey(
                        name: "FK__BacSi__sMaTaiKho__1CF15040",
                        column: x => x.sMaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "sMaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    sMaNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sHoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dNgaySinh = table.Column<DateOnly>(type: "date", nullable: false),
                    bGioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    sSDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    smDiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sMaTaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__47E09BE3839AF2D6", x => x.sMaNguoiDung);
                    table.ForeignKey(
                        name: "FK__NguoiDung__sMaTa__15502E78",
                        column: x => x.sMaTaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "sMaTaiKhoan");
                });

            migrationBuilder.CreateTable(
                name: "TuVan",
                columns: table => new
                {
                    sMaLich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dNgayTuVan = table.Column<DateOnly>(type: "date", nullable: true),
                    sMaBacSi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TuVan__6D2B2A52143386AE", x => x.sMaLich);
                    table.ForeignKey(
                        name: "FK__TuVan__sMaBacSi__25869641",
                        column: x => x.sMaBacSi,
                        principalTable: "BacSi",
                        principalColumn: "sMaBacSi");
                });

            migrationBuilder.CreateTable(
                name: "TuVan_BangThoiGian",
                columns: table => new
                {
                    sMaLich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sMaThoiGianTuVan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sGhiChuBacSi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sGhiChuNguoiDuocTuVan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    iDanhGia = table.Column<int>(type: "int", nullable: true),
                    sMaNguoiDung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sMaTrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TuVan_Ba__5ED7816151539FE3", x => new { x.sMaLich, x.sMaThoiGianTuVan });
                    table.ForeignKey(
                        name: "FK__TuVan_Ban__sMaLi__2A4B4B5E",
                        column: x => x.sMaLich,
                        principalTable: "TuVan",
                        principalColumn: "sMaLich");
                    table.ForeignKey(
                        name: "FK__TuVan_Ban__sMaNg__2B3F6F95",
                        column: x => x.sMaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "sMaNguoiDung");
                    table.ForeignKey(
                        name: "FK__TuVan_Ban__sMaTh__2B3F6F97",
                        column: x => x.sMaThoiGianTuVan,
                        principalTable: "BangThoiGianTuVan",
                        principalColumn: "sMaThoiGianTuVan");
                    table.ForeignKey(
                        name: "FK__TuVan_Ban__sMaTrangT__276EDEB3",
                        column: x => x.sMaTrangThai,
                        principalTable: "TrangThaiTuVan",
                        principalColumn: "sMaTrangThai");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BacSi_sMaChuyenKhoa",
                table: "BacSi",
                column: "sMaChuyenKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_BacSi_sMaDonViCongTac",
                table: "BacSi",
                column: "sMaDonViCongTac");

            migrationBuilder.CreateIndex(
                name: "IX_BacSi_sMaTaiKhoan",
                table: "BacSi",
                column: "sMaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_sMaTaiKhoan",
                table: "NguoiDung",
                column: "sMaTaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_sMaQuyen",
                table: "TaiKhoan",
                column: "sMaQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_TuVan_sMaBacSi",
                table: "TuVan",
                column: "sMaBacSi");

            migrationBuilder.CreateIndex(
                name: "IX_TuVan_BangThoiGian_sMaNguoiDung",
                table: "TuVan_BangThoiGian",
                column: "sMaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_TuVan_BangThoiGian_sMaThoiGianTuVan",
                table: "TuVan_BangThoiGian",
                column: "sMaThoiGianTuVan");

            migrationBuilder.CreateIndex(
                name: "IX_TuVan_BangThoiGian_sMaTrangThai",
                table: "TuVan_BangThoiGian",
                column: "sMaTrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "TuVan_BangThoiGian");

            migrationBuilder.DropTable(
                name: "TuVan");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "BangThoiGianTuVan");

            migrationBuilder.DropTable(
                name: "TrangThaiTuVan");

            migrationBuilder.DropTable(
                name: "BacSi");

            migrationBuilder.DropTable(
                name: "ChuyenKhoa");

            migrationBuilder.DropTable(
                name: "DonViCongTac");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "Quyen");
        }
    }
}
