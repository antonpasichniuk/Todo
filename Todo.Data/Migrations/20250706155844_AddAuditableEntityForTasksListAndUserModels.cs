using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntityForTasksListAndUserModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "tasks_lists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "tasks_lists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "tasks_lists");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "tasks_lists");
        }
    }
}
