using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToSoftware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Softwares",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 1,
                column: "Price",
                value: 200.22m);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 2,
                column: "Price",
                value: 1000.99m);

            migrationBuilder.UpdateData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 3,
                column: "Price",
                value: 720.59m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Softwares");
        }
    }
}
