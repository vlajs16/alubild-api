using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class OrderItemModifiedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Typologies_TypologyId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_TypologyId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TypologyId",
                table: "OrderItems");

            migrationBuilder.AddColumn<long>(
                name: "TypologyModelId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypologyModelTypologyId",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TypologyModelId_TypologyModelTypologyId",
                table: "OrderItems",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "OrderItems",
                columns: new[] { "TypologyModelId", "TypologyModelTypologyId" },
                principalTable: "TypologyModels",
                principalColumns: new[] { "Id", "TypologyId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_TypologyModels_TypologyModelId_TypologyModelTypologyId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_TypologyModelId_TypologyModelTypologyId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TypologyModelId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TypologyModelTypologyId",
                table: "OrderItems");

            migrationBuilder.AddColumn<long>(
                name: "TypologyId",
                table: "OrderItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_TypologyId",
                table: "OrderItems",
                column: "TypologyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Typologies_TypologyId",
                table: "OrderItems",
                column: "TypologyId",
                principalTable: "Typologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
