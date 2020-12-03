using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class NameChangedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypologyCategories_Categories_CategoryId",
                table: "TypologyCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TypologyCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories");

            migrationBuilder.RenameTable(
                name: "TypologyCategories",
                newName: "TypologyModelCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TypologyCategories_CategoryId",
                table: "TypologyModelCategories",
                newName: "IX_TypologyModelCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyModelCategories",
                table: "TypologyModelCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyModelCategories_Categories_CategoryId",
                table: "TypologyModelCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyModelCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyModelCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId" },
                principalTable: "TypologyModels",
                principalColumns: new[] { "Id", "TypologyId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypologyModelCategories_Categories_CategoryId",
                table: "TypologyModelCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_TypologyModelCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyModelCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyModelCategories",
                table: "TypologyModelCategories");

            migrationBuilder.RenameTable(
                name: "TypologyModelCategories",
                newName: "TypologyCategories");

            migrationBuilder.RenameIndex(
                name: "IX_TypologyModelCategories_CategoryId",
                table: "TypologyCategories",
                newName: "IX_TypologyCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyCategories_Categories_CategoryId",
                table: "TypologyCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId" },
                principalTable: "TypologyModels",
                principalColumns: new[] { "Id", "TypologyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
