using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeChemaName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "todo");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "users",
                newSchema: "todo");

            migrationBuilder.RenameTable(
                name: "tasks_lists",
                newName: "tasks_lists",
                newSchema: "todo");

            migrationBuilder.RenameTable(
                name: "tasks_list_accesses",
                newName: "tasks_list_accesses",
                newSchema: "todo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users",
                schema: "todo",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "tasks_lists",
                schema: "todo",
                newName: "tasks_lists");

            migrationBuilder.RenameTable(
                name: "tasks_list_accesses",
                schema: "todo",
                newName: "tasks_list_accesses");
        }
    }
}
