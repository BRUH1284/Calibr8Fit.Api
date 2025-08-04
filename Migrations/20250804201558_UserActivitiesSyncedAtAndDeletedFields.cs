using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserActivitiesSyncedAtAndDeletedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "base_activities",
                newName: "synced_at");

            migrationBuilder.AddColumn<bool>(
                name: "deleted",
                table: "base_activities",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "base_activities",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted",
                table: "base_activities");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "base_activities");

            migrationBuilder.RenameColumn(
                name: "synced_at",
                table: "base_activities",
                newName: "updated_at");
        }
    }
}
