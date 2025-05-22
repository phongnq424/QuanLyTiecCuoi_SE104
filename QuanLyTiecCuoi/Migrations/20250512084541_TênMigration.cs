using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class TênMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TongTienMenu",
                table: "HoaDons");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "Sanhs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "MonAns",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "DichVus",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "Sanhs");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "MonAns");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "DichVus");

            migrationBuilder.AddColumn<decimal>(
                name: "TongTienMenu",
                table: "HoaDons",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
