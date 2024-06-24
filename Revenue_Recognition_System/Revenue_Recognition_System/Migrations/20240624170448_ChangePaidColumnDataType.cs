using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    /// <inheritdoc />
    public partial class ChangePaidColumnDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Paid",
                table: "Contracts",
                type: "money",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Paid",
                table: "Contracts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
