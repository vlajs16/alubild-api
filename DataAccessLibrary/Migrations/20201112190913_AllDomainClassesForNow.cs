using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class AllDomainClassesForNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Important",
                table: "OrderPhotos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorString",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GuideId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "QualityId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Side",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TabakeraId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypologyId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlassPackages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassPackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlassQualities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassQualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tabakeras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabakeras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Typologies",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Glass = table.Column<bool>(nullable: false),
                    Guide = table.Column<bool>(nullable: false),
                    Tabakera = table.Column<bool>(nullable: false),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualities_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlassPackageTypologies",
                columns: table => new
                {
                    GlassPackageId = table.Column<long>(nullable: false),
                    TypologyId = table.Column<long>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TypologyCategories",
                columns: table => new
                {
                    TypologyId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypologyCategories", x => new { x.TypologyId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TypologyCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypologyCategories_Typologies_TypologyId",
                        column: x => x.TypologyId,
                        principalTable: "Typologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => new { x.Id, x.ManufacturerId });
                    table.ForeignKey(
                        name: "FK_Series_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CategoryId",
                table: "OrderItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ColorId",
                table: "OrderItems",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GuideId",
                table: "OrderItems",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_QualityId",
                table: "OrderItems",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TabakeraId",
                table: "OrderItems",
                column: "TabakeraId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TypologyId",
                table: "OrderItems",
                column: "TypologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CategoryId",
                table: "Colors",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GlassPackageTypologies_GlassPackageId",
                table: "GlassPackageTypologies",
                column: "GlassPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CategoryId",
                table: "Manufacturers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualities_CategoryId",
                table: "Qualities",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ManufacturerId",
                table: "Series",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypologyCategories_CategoryId",
                table: "TypologyCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Categories_CategoryId",
                table: "OrderItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Colors_ColorId",
                table: "OrderItems",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Guides_GuideId",
                table: "OrderItems",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Qualities_QualityId",
                table: "OrderItems",
                column: "QualityId",
                principalTable: "Qualities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Tabakeras_TabakeraId",
                table: "OrderItems",
                column: "TabakeraId",
                principalTable: "Tabakeras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Typologies_TypologyId",
                table: "OrderItems",
                column: "TypologyId",
                principalTable: "Typologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Categories_CategoryId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Colors_ColorId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Guides_GuideId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Qualities_QualityId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Tabakeras_TabakeraId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Typologies_TypologyId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "GlassPackageTypologies");

            migrationBuilder.DropTable(
                name: "GlassQualities");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Tabakeras");

            migrationBuilder.DropTable(
                name: "TypologyCategories");

            migrationBuilder.DropTable(
                name: "GlassPackages");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Typologies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CategoryId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ColorId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_GuideId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_QualityId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_TabakeraId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_TypologyId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Important",
                table: "OrderPhotos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ColorString",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "QualityId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Side",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TabakeraId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TypologyId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AspNetUsers");
        }
    }
}
