using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class addColumnsForLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonContent",
                table: "Lessons");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "Lessons",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "FileText",
                table: "Lessons",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LessonContentName",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "FileText",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonContentName",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "LessonContent",
                table: "Lessons",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
