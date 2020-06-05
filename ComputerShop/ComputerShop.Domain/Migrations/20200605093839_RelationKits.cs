using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerShop.Domain.Migrations
{
    public partial class RelationKits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kits_CategoryId",
                table: "Kits");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_CategoryId",
                table: "Kits",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Kits_CategoryId",
                table: "Kits");

            migrationBuilder.CreateIndex(
                name: "IX_Kits_CategoryId",
                table: "Kits",
                column: "CategoryId",
                unique: true);
        }
    }
}
