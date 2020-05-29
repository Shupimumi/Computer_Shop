using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerShop.Domain.Migrations
{
    public partial class RenameDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_Kits_KitID",
                table: "Description");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Description",
                table: "Description");

            migrationBuilder.RenameTable(
                name: "Description",
                newName: "Descriptions");

            migrationBuilder.RenameIndex(
                name: "IX_Description_KitID",
                table: "Descriptions",
                newName: "IX_Descriptions_KitID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Descriptions",
                table: "Descriptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptions_Kits_KitID",
                table: "Descriptions",
                column: "KitID",
                principalTable: "Kits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Descriptions_Kits_KitID",
                table: "Descriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Descriptions",
                table: "Descriptions");

            migrationBuilder.RenameTable(
                name: "Descriptions",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Descriptions_KitID",
                table: "Description",
                newName: "IX_Description_KitID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Description",
                table: "Description",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Description_Kits_KitID",
                table: "Description",
                column: "KitID",
                principalTable: "Kits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
