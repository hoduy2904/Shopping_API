using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    public partial class fixCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductVariations_ProductVariationId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductVariationId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductVariationId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVarationId",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 2, 15, 46, 9, 49, DateTimeKind.Local).AddTicks(3300));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 2, 15, 46, 9, 49, DateTimeKind.Local).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 2, 15, 46, 9, 49, DateTimeKind.Local).AddTicks(4142));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductVarationId",
                table: "Carts",
                column: "ProductVarationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductVariations_ProductVarationId",
                table: "Carts",
                column: "ProductVarationId",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ProductVariations_ProductVarationId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ProductVarationId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVarationId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Carts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductVariationId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 31, 11, 14, 9, 324, DateTimeKind.Local).AddTicks(2896));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 31, 11, 14, 9, 324, DateTimeKind.Local).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 31, 11, 14, 9, 324, DateTimeKind.Local).AddTicks(3706));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductVariationId",
                table: "Carts",
                column: "ProductVariationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ProductVariations_ProductVariationId",
                table: "Carts",
                column: "ProductVariationId",
                principalTable: "ProductVariations",
                principalColumn: "Id");
        }
    }
}
