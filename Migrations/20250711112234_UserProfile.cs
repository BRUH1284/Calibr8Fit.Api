using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activity_level",
                table: "user_profiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "climate",
                table: "user_profiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "user_profiles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "gender",
                table: "user_profiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "goal",
                table: "user_profiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "height",
                table: "user_profiles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "target_weight",
                table: "user_profiles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "weight",
                table: "user_profiles",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_level",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "climate",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "goal",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "height",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "target_weight",
                table: "user_profiles");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "user_profiles");
        }
    }
}
