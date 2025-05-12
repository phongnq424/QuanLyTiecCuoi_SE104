using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BaoCaoThangs",
                columns: table => new
                {
                    MaBaoCao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    TongDoanhThu = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TongTiecCuoi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoThangs", x => x.MaBaoCao);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CaSanhs",
                columns: table => new
                {
                    MaCa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenCa = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GioBatDau = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GioKetThuc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaSanhs", x => x.MaCa);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChucNangs",
                columns: table => new
                {
                    MaChucNang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenChucNang = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenManHinhDuocLoad = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucNangs", x => x.MaChucNang);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    MaDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenDichVu = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DonGia = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MoTa = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.MaDichVu);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoaiSanhs",
                columns: table => new
                {
                    MaLoaiSanh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenLoaiSanh = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DonGiaBanToiThieu = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSanhs", x => x.MaLoaiSanh);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenMon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DonGia = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MaMon);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NhomNguoiDungs",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenNhom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomNguoiDungs", x => x.MaNhom);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ThamSos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TyLePhatThanhToanTreTheoNgay = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ApDungQDPhatThanhToanTre = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamSos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietBaoCaos",
                columns: table => new
                {
                    MaChiTietBaoCao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaBaoCao = table.Column<int>(type: "int", nullable: false),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SoLuongTiecCuoi = table.Column<int>(type: "int", nullable: false),
                    DoanhThu = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TyLe = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietBaoCaos", x => x.MaChiTietBaoCao);
                    table.ForeignKey(
                        name: "FK_ChiTietBaoCaos_BaoCaoThangs_MaBaoCao",
                        column: x => x.MaBaoCao,
                        principalTable: "BaoCaoThangs",
                        principalColumn: "MaBaoCao",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sanhs",
                columns: table => new
                {
                    MaSanh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenSanh = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaLoaiSanh = table.Column<int>(type: "int", nullable: false),
                    SoLuongBanToiDa = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanhs", x => x.MaSanh);
                    table.ForeignKey(
                        name: "FK_Sanhs_LoaiSanhs_MaLoaiSanh",
                        column: x => x.MaLoaiSanh,
                        principalTable: "LoaiSanhs",
                        principalColumn: "MaLoaiSanh",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    TenDangNhap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MatKhau = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaNhom = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.TenDangNhap);
                    table.ForeignKey(
                        name: "FK_NguoiDungs_NhomNguoiDungs_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomNguoiDungs",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PhanQuyens",
                columns: table => new
                {
                    MaNhom = table.Column<int>(type: "int", nullable: false),
                    MaChucNang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanQuyens", x => new { x.MaNhom, x.MaChucNang });
                    table.ForeignKey(
                        name: "FK_PhanQuyens_ChucNangs_MaChucNang",
                        column: x => x.MaChucNang,
                        principalTable: "ChucNangs",
                        principalColumn: "MaChucNang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanQuyens_NhomNguoiDungs_MaNhom",
                        column: x => x.MaNhom,
                        principalTable: "NhomNguoiDungs",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DatTiecs",
                columns: table => new
                {
                    MaDatTiec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TenCoDau = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TenChuRe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SDT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaSanh = table.Column<int>(type: "int", nullable: false),
                    MaCa = table.Column<int>(type: "int", nullable: false),
                    TienDatCoc = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    SoLuongBan = table.Column<int>(type: "int", nullable: false),
                    SoBanDuTru = table.Column<int>(type: "int", nullable: false),
                    NgayDaiTiec = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gio = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatTiecs", x => x.MaDatTiec);
                    table.ForeignKey(
                        name: "FK_DatTiecs_CaSanhs_MaCa",
                        column: x => x.MaCa,
                        principalTable: "CaSanhs",
                        principalColumn: "MaCa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatTiecs_Sanhs_MaSanh",
                        column: x => x.MaSanh,
                        principalTable: "Sanhs",
                        principalColumn: "MaSanh",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietDVTiecs",
                columns: table => new
                {
                    MaCTDV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaDatTiec = table.Column<int>(type: "int", nullable: false),
                    MaDichVu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDVTiecs", x => x.MaCTDV);
                    table.ForeignKey(
                        name: "FK_ChiTietDVTiecs_DatTiecs_MaDatTiec",
                        column: x => x.MaDatTiec,
                        principalTable: "DatTiecs",
                        principalColumn: "MaDatTiec",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDVTiecs_DichVus_MaDichVu",
                        column: x => x.MaDichVu,
                        principalTable: "DichVus",
                        principalColumn: "MaDichVu",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChiTietMenus",
                columns: table => new
                {
                    MaCTMN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MaDatTiec = table.Column<int>(type: "int", nullable: false),
                    MaMon = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietMenus", x => x.MaCTMN);
                    table.ForeignKey(
                        name: "FK_ChiTietMenus_DatTiecs_MaDatTiec",
                        column: x => x.MaDatTiec,
                        principalTable: "DatTiecs",
                        principalColumn: "MaDatTiec",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietMenus_MonAns_MaMon",
                        column: x => x.MaMon,
                        principalTable: "MonAns",
                        principalColumn: "MaMon",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DonGiaBan = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TongTienBan = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaDatTiec = table.Column<int>(type: "int", nullable: false),
                    TongTienMenu = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TongTienDV = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TongTienHD = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TienPhaiThanhToan = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_DatTiecs_MaDatTiec",
                        column: x => x.MaDatTiec,
                        principalTable: "DatTiecs",
                        principalColumn: "MaDatTiec",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBaoCaos_MaBaoCao",
                table: "ChiTietBaoCaos",
                column: "MaBaoCao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDVTiecs_MaDatTiec",
                table: "ChiTietDVTiecs",
                column: "MaDatTiec");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDVTiecs_MaDichVu",
                table: "ChiTietDVTiecs",
                column: "MaDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietMenus_MaDatTiec",
                table: "ChiTietMenus",
                column: "MaDatTiec");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietMenus_MaMon",
                table: "ChiTietMenus",
                column: "MaMon");

            migrationBuilder.CreateIndex(
                name: "IX_DatTiecs_MaCa",
                table: "DatTiecs",
                column: "MaCa");

            migrationBuilder.CreateIndex(
                name: "IX_DatTiecs_MaSanh",
                table: "DatTiecs",
                column: "MaSanh");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaDatTiec",
                table: "HoaDons",
                column: "MaDatTiec");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_MaNhom",
                table: "NguoiDungs",
                column: "MaNhom");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyens_MaChucNang",
                table: "PhanQuyens",
                column: "MaChucNang");

            migrationBuilder.CreateIndex(
                name: "IX_Sanhs_MaLoaiSanh",
                table: "Sanhs",
                column: "MaLoaiSanh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietBaoCaos");

            migrationBuilder.DropTable(
                name: "ChiTietDVTiecs");

            migrationBuilder.DropTable(
                name: "ChiTietMenus");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "PhanQuyens");

            migrationBuilder.DropTable(
                name: "ThamSos");

            migrationBuilder.DropTable(
                name: "BaoCaoThangs");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropTable(
                name: "MonAns");

            migrationBuilder.DropTable(
                name: "DatTiecs");

            migrationBuilder.DropTable(
                name: "ChucNangs");

            migrationBuilder.DropTable(
                name: "NhomNguoiDungs");

            migrationBuilder.DropTable(
                name: "CaSanhs");

            migrationBuilder.DropTable(
                name: "Sanhs");

            migrationBuilder.DropTable(
                name: "LoaiSanhs");
        }
    }
}
