using Microsoft.EntityFrameworkCore.Migrations;

namespace BolsaDeValores.Migrations
{
    public partial class ActionswithCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Actions",
                newName: "IdCategory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Actions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actions_CategoryId",
                table: "Actions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Categories_CategoryId",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_CategoryId",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "Actions",
                newName: "Name");
        }
    }
}
