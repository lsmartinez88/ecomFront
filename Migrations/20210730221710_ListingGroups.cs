using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class ListingGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemGrouping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    GroupDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GroupingType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdGrouping = table.Column<int>(type: "int", nullable: false),
                    Nameml = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemGrouping", x => x.Id);
                    table.UniqueConstraint("AK_ItemGrouping_GroupingType_IdGrouping", x => new { x.GroupingType, x.IdGrouping });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "front_listing_group",
                columns: table => new
                {
                    CriteriaId = table.Column<int>(type: "int", nullable: false),
                    ExecutionId = table.Column<int>(type: "int", nullable: false),
                    GroupingType = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ItemGroupingId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_front_listing_group", x => new { x.CriteriaId, x.ExecutionId, x.GroupingType, x.ItemGroupingId });
                    table.ForeignKey(
                        name: "FK_front_listing_group_ItemGrouping_ItemGroupingId",
                        column: x => x.ItemGroupingId,
                        principalTable: "ItemGrouping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_front_listing_group_ItemGroupingId",
                table: "front_listing_group",
                column: "ItemGroupingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "front_listing_group");

            migrationBuilder.DropTable(
                name: "ItemGrouping");
        }
    }
}
