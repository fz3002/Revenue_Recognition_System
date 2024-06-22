using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnPeselAndKRS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_People_Pesel",
                table: "People",
                column: "Pesel",
                unique: true,
                filter: "[Pesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_KRS",
                table: "Companies",
                column: "KRS",
                unique: true,
                filter: "[KRS] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_People_Pesel",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_Companies_KRS",
                table: "Companies");
        }
    }
}
