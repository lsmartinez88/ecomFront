using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class ColumnaCantidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadVentas",
                table: "front_average_price_per_day",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadVentas",
                table: "front_average_price_per_day");
        }
    }
}
