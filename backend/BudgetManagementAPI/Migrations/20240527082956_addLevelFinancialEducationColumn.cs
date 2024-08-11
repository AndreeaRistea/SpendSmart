using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addLevelFinancialEducationColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LevelFinancialEducation",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelFinancialEducation",
                table: "Users");
        }
    }
}
