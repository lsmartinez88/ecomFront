using Microsoft.EntityFrameworkCore.Migrations;

namespace ecomFront.Migrations
{
    public partial class AddUserInActivityLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "front_activity_information",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_front_activity_information_userId",
                table: "front_activity_information",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_front_activity_information_AspNetUsers_userId",
                table: "front_activity_information",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_front_activity_information_AspNetUsers_userId",
                table: "front_activity_information");

            migrationBuilder.DropIndex(
                name: "IX_front_activity_information_userId",
                table: "front_activity_information");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "front_activity_information");
        }
    }
}
