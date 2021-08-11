using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class AveragePricePerDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "front_average_price_per_day",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PrecioMedio = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_average_price_per_day", x => new { x.CriteriaId, x.ExecutionId, x.Fecha });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "front_average_price_per_day");
        }
    }
}
