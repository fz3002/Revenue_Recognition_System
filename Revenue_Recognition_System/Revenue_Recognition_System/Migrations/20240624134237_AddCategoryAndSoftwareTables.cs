using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndSoftwareTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    IdSoftware = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.IdSoftware);
                    table.ForeignKey(
                        name: "FK_Softwares_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "Name" },
                values: new object[,]
                {
                    { 1, "Productivity" },
                    { 2, "Development" },
                    { 3, "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "IdSoftware", "Description", "IdCategory", "Name", "Version" },
                values: new object[,]
                {
                    { 1, "IDE for development", 2, "Visual Studio", "2022" },
                    { 2, "Text editor", 2, "Notepad++", "8.1.9" },
                    { 3, "Music streaming", 3, "Spotify", "1.1.72" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_IdCategory",
                table: "Softwares",
                column: "IdCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
