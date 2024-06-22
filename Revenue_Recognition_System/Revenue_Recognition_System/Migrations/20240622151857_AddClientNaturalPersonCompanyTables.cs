using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddClientNaturalPersonCompanyTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    KRS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_Companies_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_People_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "IdClient", "Address", "Email", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "info@companyone.com", "123-456-7890" },
                    { 2, "456 Elm St", "info@companytwo.com", "098-765-4321" },
                    { 3, "123 Main St", "john.doe@example.com", "123-456-7890" },
                    { 4, "456 Elm St", "jane.smith@example.com", "098-765-4321" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "IdClient", "CompanyName", "KRS" },
                values: new object[,]
                {
                    { 1, "Company One", "1234567890" },
                    { 2, "Company Two", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "IdClient", "Name", "Pesel", "Surname" },
                values: new object[,]
                {
                    { 3, "John", "12345678901", "Doe" },
                    { 4, "Jane", "09876543210", "Smith" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
