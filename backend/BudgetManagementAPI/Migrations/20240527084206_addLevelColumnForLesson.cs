using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addLevelColumnForLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LevelName",
                table: "LevelLessons");

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "LevelLessons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "LevelLessons");

            migrationBuilder.AddColumn<string>(
                name: "LevelName",
                table: "LevelLessons",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
