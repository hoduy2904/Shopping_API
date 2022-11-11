using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    public partial class editDeliveryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "InfomationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "InfomationUsers");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 17, 7, 6, 541, DateTimeKind.Local).AddTicks(3911));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 17, 7, 6, 541, DateTimeKind.Local).AddTicks(4790));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 17, 7, 6, 541, DateTimeKind.Local).AddTicks(4726));
        }
    }
}
