using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSeedingDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 1,
                column: "Value",
                value: 0.5m);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 2,
                column: "Value",
                value: 0.7m);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 3,
                column: "Value",
                value: 0.3m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 1,
                column: "Value",
                value: 50.0m);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 2,
                column: "Value",
                value: 70.0m);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 3,
                column: "Value",
                value: 30.0m);
        }
    }
}
