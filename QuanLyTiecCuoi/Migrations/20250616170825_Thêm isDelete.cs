using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class ThêmisDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "Sanhs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "MonAns",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "LoaiSanhs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "DichVus",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "DatTiecs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "ChiTietMenus",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "ChiTietDVTiecs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDelelte",
                table: "CaSanhs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "Sanhs");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "MonAns");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "LoaiSanhs");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "DichVus");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "DatTiecs");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "ChiTietMenus");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "ChiTietDVTiecs");

            migrationBuilder.DropColumn(
                name: "isDelelte",
                table: "CaSanhs");
        }
    }
}
