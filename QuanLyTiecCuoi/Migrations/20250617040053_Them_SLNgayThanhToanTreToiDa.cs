using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class Them_SLNgayThanhToanTreToiDa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SLNgayThanhToanTreToiDa",
                table: "ThamSos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLNgayThanhToanTreToiDa",
                table: "ThamSos");
        }
    }
}
