using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserActivitiesUpdatedAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "user_activities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "user_activities");
        }
    }
}
