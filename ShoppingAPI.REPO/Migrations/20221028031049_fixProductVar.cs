using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    public partial class fixProductVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariations_ProductVariations_ProductVariateId",
                table: "ProductVariations");

            migrationBuilder.DropColumn(
                name: "Variation",
                table: "ProductVariations");

            migrationBuilder.RenameColumn(
                name: "ProductVariateId",
                table: "ProductVariations",
                newName: "VariationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariations_ProductVariateId",
                table: "ProductVariations",
                newName: "IX_ProductVariations_VariationId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 28, 10, 10, 49, 190, DateTimeKind.Local).AddTicks(4035));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 28, 10, 10, 49, 190, DateTimeKind.Local).AddTicks(4888));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 28, 10, 10, 49, 190, DateTimeKind.Local).AddTicks(4828));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariations_ProductVariations_VariationId",
                table: "ProductVariations",
                column: "VariationId",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariations_ProductVariations_VariationId",
                table: "ProductVariations");

            migrationBuilder.RenameColumn(
                name: "VariationId",
                table: "ProductVariations",
                newName: "ProductVariateId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariations_VariationId",
                table: "ProductVariations",
                newName: "IX_ProductVariations_ProductVariateId");

            migrationBuilder.AddColumn<string>(
                name: "Variation",
                table: "ProductVariations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 27, 14, 34, 3, 91, DateTimeKind.Local).AddTicks(5297));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 27, 14, 34, 3, 91, DateTimeKind.Local).AddTicks(6173));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 27, 14, 34, 3, 91, DateTimeKind.Local).AddTicks(6108));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariations_ProductVariations_ProductVariateId",
                table: "ProductVariations",
                column: "ProductVariateId",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }
    }
}
