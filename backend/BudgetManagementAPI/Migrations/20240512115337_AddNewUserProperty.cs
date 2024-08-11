using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewUserProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Users");
        }
    }
}
