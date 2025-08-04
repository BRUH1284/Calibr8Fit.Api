using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class ActivitiesTPH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_activities_users_user_id",
                table: "user_activities");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_activities",
                table: "user_activities");

            migrationBuilder.RenameTable(
                name: "user_activities",
                newName: "base_activities");

            migrationBuilder.RenameIndex(
                name: "ix_user_activities_user_id_id",
                table: "base_activities",
                newName: "ix_base_activities_user_id_id");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "base_activities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "base_activities",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "is_user_activity",
                table: "base_activities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_base_activities",
                table: "base_activities",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_base_activities_asp_net_users_user_id",
                table: "base_activities",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_base_activities_asp_net_users_user_id",
                table: "base_activities");

            migrationBuilder.DropPrimaryKey(
                name: "pk_base_activities",
                table: "base_activities");

            migrationBuilder.DropColumn(
                name: "is_user_activity",
                table: "base_activities");

            migrationBuilder.RenameTable(
                name: "base_activities",
                newName: "user_activities");

            migrationBuilder.RenameIndex(
                name: "ix_base_activities_user_id_id",
                table: "user_activities",
                newName: "ix_user_activities_user_id_id");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "user_activities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "user_activities",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_activities",
                table: "user_activities",
                column: "id");

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    major_heading = table.Column<string>(type: "text", nullable: false),
                    met_value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activities", x => x.code);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_user_activities_users_user_id",
                table: "user_activities",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
