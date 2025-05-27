using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeRestaurant_.Migrations
{
    public partial class AddPieceToEnvanter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Piece",
                table: "Envanters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Piece",
                table: "Envanters");
        }
    }
}
