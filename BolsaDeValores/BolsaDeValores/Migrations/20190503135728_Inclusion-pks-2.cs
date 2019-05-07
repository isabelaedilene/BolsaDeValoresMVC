using Microsoft.EntityFrameworkCore.Migrations;

namespace BolsaDeValores.Migrations
{
    public partial class Inclusionpks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idCategory",
                table: "Actions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idOwner",
                table: "Actions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idCategory",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "idOwner",
                table: "Actions");
        }
    }
}
