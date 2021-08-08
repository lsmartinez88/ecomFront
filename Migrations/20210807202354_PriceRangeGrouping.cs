using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class PriceRangeGrouping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "front_price_range_grouping",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    RangoDesde = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RangoHasta = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupingType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemGroupingId = table.Column<int>(type: "int", nullable: false),
                    CantidadPublicaciones = table.Column<int>(type: "int", nullable: false),
                    CantidadVentas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_price_range_grouping", x => new { x.CriteriaId, x.ExecutionId, x.RangoDesde, x.RangoHasta, x.GroupingType, x.ItemGroupingId });
                    table.ForeignKey(
                        name: "FK_front_price_range_grouping_ItemGrouping_ItemGroupingId",
                        column: x => x.ItemGroupingId,
                        principalTable: "ItemGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_front_price_range_grouping_ItemGroupingId",
                table: "front_price_range_grouping",
                column: "ItemGroupingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "front_price_range_grouping");
        }
    }
}
