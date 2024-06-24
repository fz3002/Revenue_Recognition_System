using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscountAndDiscountTypeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    IdDiscountType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.IdDiscountType);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    IdDiscount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdDiscountType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.IdDiscount);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountTypes_IdDiscountType",
                        column: x => x.IdDiscountType,
                        principalTable: "DiscountTypes",
                        principalColumn: "IdDiscountType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "IdDiscountType", "Name" },
                values: new object[,]
                {
                    { 1, "Discount for one time purchase" },
                    { 2, "Discount for subscription" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "DateFrom", "DateTo", "IdDiscountType", "Name", "Value" },
                values: new object[,]
                {
                    { 1, new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 30), 1, "Summer Sale", 50.0m },
                    { 2, new DateOnly(2024, 11, 25), new DateOnly(2024, 11, 30), 1, "Black Friday", 70.0m },
                    { 3, new DateOnly(2024, 12, 20), new DateOnly(2024, 12, 25), 1, "Christmas Sale", 30.0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_IdDiscountType",
                table: "Discounts",
                column: "IdDiscountType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "DiscountTypes");
        }
    }
}
