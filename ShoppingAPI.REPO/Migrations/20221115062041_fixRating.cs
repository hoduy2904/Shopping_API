using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    public partial class fixRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "ProductRatings",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 15, 13, 20, 41, 269, DateTimeKind.Local).AddTicks(9241));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 15, 13, 20, 41, 270, DateTimeKind.Local).AddTicks(363));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 15, 13, 20, 41, 270, DateTimeKind.Local).AddTicks(287));

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_InvoiceId",
                table: "ProductRatings",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductRatings_invoices_InvoiceId",
                table: "ProductRatings",
                column: "InvoiceId",
                principalTable: "invoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductRatings_invoices_InvoiceId",
                table: "ProductRatings");

            migrationBuilder.DropIndex(
                name: "IX_ProductRatings_InvoiceId",
                table: "ProductRatings");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "ProductRatings");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 11, 10, 22, 38, 960, DateTimeKind.Local).AddTicks(1438));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 11, 10, 22, 38, 960, DateTimeKind.Local).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 11, 10, 22, 38, 960, DateTimeKind.Local).AddTicks(2311));
        }
    }
}
