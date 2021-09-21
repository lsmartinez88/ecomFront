using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class tablabarchartoportunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "front_bar_chart_oportunity",
                columns: table => new
                {
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    Palabra = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IndicadorOportunidad = table.Column<double>(type: "double", nullable: true),
                    CantidadAparicionesTendencia = table.Column<int>(type: "int", nullable: true),
                    PosicionMejorTendencia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_bar_chart_oportunity", x => new { x.ExecutionId, x.Palabra });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "front_bar_chart_oportunity");
        }
    }
}
