using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class Them_SLB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoLuongBan",
                table: "HoaDons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoLuongBan",
                table: "HoaDons");
        }
    }
}
