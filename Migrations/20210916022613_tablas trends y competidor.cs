using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class tablastrendsycompetidor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "front_top_sellers_info",
                columns: table => new
                {
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CantidadVentas = table.Column<int>(type: "int", nullable: false),
                    CantidadPublicaciones = table.Column<int>(type: "int", nullable: false),
                    CantidadPreguntas = table.Column<int>(type: "int", nullable: false),
                    CantidadVisitas = table.Column<int>(type: "int", nullable: false),
                    CantidadReviews = table.Column<int>(type: "int", nullable: false),
                    ZonaMayorVenta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ZonaMayorVentaCantidad = table.Column<int>(type: "int", nullable: false),
                    PublicacionMayorVenta = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublicacionMayorVentaCantidad = table.Column<int>(type: "int", nullable: false),
                    CancelacionesHistorico = table.Column<int>(type: "int", nullable: false),
                    RatingNegativo = table.Column<double>(type: "double", nullable: false),
                    RatingPositivo = table.Column<double>(type: "double", nullable: false),
                    RatingNeutral = table.Column<double>(type: "double", nullable: false),
                    ClaimsRate = table.Column<double>(type: "double", nullable: false),
                    ClaimsValue = table.Column<int>(type: "int", nullable: false),
                    DelayedRate = table.Column<double>(type: "double", nullable: false),
                    DelayedValue = table.Column<int>(type: "int", nullable: false),
                    CancellationRate = table.Column<double>(type: "double", nullable: false),
                    CancellationValue = table.Column<int>(type: "int", nullable: false),
                    TipoVendedor = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosicionVendedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_top_sellers_info", x => new { x.ExecutionId, x.SellerId });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "front_trends_treemap",
                columns: table => new
                {
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    TrendName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrendPadre = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IndiceSize = table.Column<double>(type: "double", nullable: true),
                    IndiceColor = table.Column<double>(type: "double", nullable: true),
                    CantidadVentas = table.Column<int>(type: "int", nullable: true),
                    CantidadPublicaciones = table.Column<int>(type: "int", nullable: true),
                    PosicionTendencia = table.Column<int>(type: "int", nullable: false),
                    VentasPorPublicacion = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_trends_treemap", x => new { x.ExecutionId, x.TrendName, x.TrendPadre });
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "front_top_sellers_info");

            migrationBuilder.DropTable(
                name: "front_trends_treemap");
        }
    }
}
