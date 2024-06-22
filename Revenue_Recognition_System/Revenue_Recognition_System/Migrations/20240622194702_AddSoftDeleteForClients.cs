using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteForClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "People",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "IdClient",
                keyValue: 3,
                columns: new[] { "DeletedAt", "IsDeleted" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "IdClient",
                keyValue: 4,
                columns: new[] { "DeletedAt", "IsDeleted" },
                values: new object[] { null, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "People");
        }
    }
}
