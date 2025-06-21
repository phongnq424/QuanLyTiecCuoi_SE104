using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class SuaIsDeleteThanhTinhTrang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDelelte",
                table: "Sanhs",
                newName: "TinhTrang");

            migrationBuilder.RenameColumn(
                name: "isDelelte",
                table: "MonAns",
                newName: "TinhTrang");

            migrationBuilder.RenameColumn(
                name: "isDelelte",
                table: "LoaiSanhs",
                newName: "TinhTrang");

            migrationBuilder.RenameColumn(
                name: "isDelelte",
                table: "DichVus",
                newName: "TinhTrang");

            migrationBuilder.RenameColumn(
                name: "isDelelte",
                table: "CaSanhs",
                newName: "TinhTrang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "Sanhs",
                newName: "isDelelte");

            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "MonAns",
                newName: "isDelelte");

            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "LoaiSanhs",
                newName: "isDelelte");

            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "DichVus",
                newName: "isDelelte");

            migrationBuilder.RenameColumn(
                name: "TinhTrang",
                table: "CaSanhs",
                newName: "isDelelte");
        }
    }
}
