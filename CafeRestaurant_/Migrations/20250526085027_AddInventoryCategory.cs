using Microsoft.EntityFrameworkCore.Migrations;

namespace CafeRestaurant_.Migrations
{
    public partial class AddInventoryCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envanters_Categories_CategoryId",
                table: "Envanters");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Envanters",
                newName: "InventoryCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Envanters_CategoryId",
                table: "Envanters",
                newName: "IX_Envanters_InventoryCategoryId");

            migrationBuilder.CreateTable(
                name: "InventoryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryCategories", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Envanters_InventoryCategories_InventoryCategoryId",
                table: "Envanters",
                column: "InventoryCategoryId",
                principalTable: "InventoryCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envanters_InventoryCategories_InventoryCategoryId",
                table: "Envanters");

            migrationBuilder.DropTable(
                name: "InventoryCategories");

            migrationBuilder.RenameColumn(
                name: "InventoryCategoryId",
                table: "Envanters",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Envanters_InventoryCategoryId",
                table: "Envanters",
                newName: "IX_Envanters_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envanters_Categories_CategoryId",
                table: "Envanters",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
