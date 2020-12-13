using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class GlassPackagesModifiedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlassPackageTypologies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlassPackageTypologies",
                columns: table => new
                {
                    TypologyId = table.Column<long>(type: "bigint", nullable: false),
                    GlassPackageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassPackageTypologies", x => new { x.TypologyId, x.GlassPackageId });
                    table.ForeignKey(
                        name: "FK_GlassPackageTypologies_GlassPackages_GlassPackageId",
                        column: x => x.GlassPackageId,
                        principalTable: "GlassPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlassPackageTypologies_Typologies_TypologyId",
                        column: x => x.TypologyId,
                        principalTable: "Typologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlassPackageTypologies_GlassPackageId",
                table: "GlassPackageTypologies",
                column: "GlassPackageId");
        }
    }
}
