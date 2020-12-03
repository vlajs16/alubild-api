using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class TypologyModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypologyCategories_Typologies_TypologyId",
                table: "TypologyCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories");

            migrationBuilder.DropColumn(
                name: "TypologyId",
                table: "TypologyCategories");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Typologies");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Typologies");

            migrationBuilder.AddColumn<long>(
                name: "TypologyModelId",
                table: "TypologyCategories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TypologyModelTypologyId",
                table: "TypologyCategories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "TypologyModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypologyId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypologyModels", x => new { x.Id, x.TypologyId });
                    table.ForeignKey(
                        name: "FK_TypologyModels_Typologies_TypologyId",
                        column: x => x.TypologyId,
                        principalTable: "Typologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TypologyModels_TypologyId",
                table: "TypologyModels",
                column: "TypologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyCategories",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId" },
                principalTable: "TypologyModels",
                principalColumns: new[] { "Id", "TypologyId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypologyCategories_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "TypologyCategories");

            migrationBuilder.DropTable(
                name: "TypologyModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories");

            migrationBuilder.DropColumn(
                name: "TypologyModelId",
                table: "TypologyCategories");

            migrationBuilder.DropColumn(
                name: "TypologyModelTypologyId",
                table: "TypologyCategories");

            migrationBuilder.AddColumn<long>(
                name: "TypologyId",
                table: "TypologyCategories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Typologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Typologies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypologyCategories",
                table: "TypologyCategories",
                columns: new[] { "TypologyId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TypologyCategories_Typologies_TypologyId",
                table: "TypologyCategories",
                column: "TypologyId",
                principalTable: "Typologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
