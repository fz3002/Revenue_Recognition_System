using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    IdContract = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    IdDiscount = table.Column<int>(type: "int", nullable: false),
                    YearsOfSupport = table.Column<int>(type: "int", nullable: false),
                    IdSoftware = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Discounts_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "Discounts",
                        principalColumn: "IdDiscount",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Softwares_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdClient",
                table: "Contracts",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdDiscount",
                table: "Contracts",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdSoftware",
                table: "Contracts",
                column: "IdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
