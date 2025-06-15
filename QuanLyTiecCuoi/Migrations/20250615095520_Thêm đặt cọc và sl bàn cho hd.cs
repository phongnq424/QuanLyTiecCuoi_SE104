using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyTiecCuoi.Migrations
{
    /// <inheritdoc />
    public partial class Thêmđặtcọcvàslbànchohd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PhanTramDatCoc",
                table: "ThamSos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhanTramDatCoc",
                table: "ThamSos");
        }
    }
}
