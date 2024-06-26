using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Discounts_IdDiscount",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contracts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "IdUser", "Login", "Password", "RefreshToken", "RefreshTokenExp", "Role", "Salt" },
                values: new object[] { 1, "admin", "WTDTVyCT802NykKe2rWsBz7rhQK30s+BqPXEaFWDSjc=", "NmmAJEtrxBqlIwR6QO6vMpiTdRarhN3ymghpsx8Ehjg=", new DateTime(2024, 6, 28, 20, 2, 56, 153, DateTimeKind.Local).AddTicks(8603), 1, "BOM69PB5Cugh+Ws2UxOoyA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Discounts_IdDiscount",
                table: "Contracts",
                column: "IdDiscount",
                principalTable: "Discounts",
                principalColumn: "IdDiscount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Discounts_IdDiscount",
                table: "Contracts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "IdDiscount",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Discounts_IdDiscount",
                table: "Contracts",
                column: "IdDiscount",
                principalTable: "Discounts",
                principalColumn: "IdDiscount",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
