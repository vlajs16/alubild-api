using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class OrderItemPropertiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GlassPackageId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GlassQualityId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SeriesId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SeriesManufacturerId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GlassPackageId",
                table: "OrderItems",
                column: "GlassPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GlassQualityId",
                table: "OrderItems",
                column: "GlassQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_SeriesId_SeriesManufacturerId",
                table: "OrderItems",
                columns: new[] { "SeriesId", "SeriesManufacturerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_GlassPackages_GlassPackageId",
                table: "OrderItems",
                column: "GlassPackageId",
                principalTable: "GlassPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_GlassQualities_GlassQualityId",
                table: "OrderItems",
                column: "GlassQualityId",
                principalTable: "GlassQualities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Series_SeriesId_SeriesManufacturerId",
                table: "OrderItems",
                columns: new[] { "SeriesId", "SeriesManufacturerId" },
                principalTable: "Series",
                principalColumns: new[] { "Id", "ManufacturerId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_GlassPackages_GlassPackageId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_GlassQualities_GlassQualityId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Series_SeriesId_SeriesManufacturerId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_GlassPackageId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_GlassQualityId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_SeriesId_SeriesManufacturerId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "GlassPackageId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "GlassQualityId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "SeriesManufacturerId",
                table: "OrderItems");
        }
    }
}
