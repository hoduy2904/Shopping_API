using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingAPI.REPO.Migrations
{
    public partial class addRatingProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductVariationId = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ProductRatingImage = table.Column<int>(type: "int", nullable: false),
                    ProductRatingId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatings_ProductRatings_ProductRatingId",
                        column: x => x.ProductRatingId,
                        principalTable: "ProductRatings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRatings_ProductVariations_ProductVariationId",
                        column: x => x.ProductVariationId,
                        principalTable: "ProductVariations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductRatings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRatingImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductRatingId = table.Column<int>(type: "int", nullable: false),
                    IsTrash = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatingImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatingImages_ProductRatings_ProductRatingId",
                        column: x => x.ProductRatingId,
                        principalTable: "ProductRatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 43, 44, 982, DateTimeKind.Local).AddTicks(3403));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 43, 44, 982, DateTimeKind.Local).AddTicks(4296));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 43, 44, 982, DateTimeKind.Local).AddTicks(4214));

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatingImages_ProductRatingId",
                table: "ProductRatingImages",
                column: "ProductRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId",
                table: "ProductRatings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductRatingId",
                table: "ProductRatings",
                column: "ProductRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductVariationId",
                table: "ProductRatings",
                column: "ProductVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_UserId",
                table: "ProductRatings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRatingImages");

            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 14, 17, 826, DateTimeKind.Local).AddTicks(9371));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 14, 17, 827, DateTimeKind.Local).AddTicks(251));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 7, 14, 14, 17, 827, DateTimeKind.Local).AddTicks(186));
        }
    }
}
