using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileSyncFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "forced_consumption_target",
                table: "user_profiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "forced_hydration_target",
                table: "user_profiles",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "user_profiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "synced_at",
                table: "user_profiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forced_consumption_target",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "forced_hydration_target",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "synced_at",
                table: "user_profiles");
        }
    }
}
