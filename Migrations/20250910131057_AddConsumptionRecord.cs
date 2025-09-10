using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddConsumptionRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumption_records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    food_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_meal_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quantity = table.Column<float>(type: "real", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    synced_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_consumption_records", x => x.id);
                    table.ForeignKey(
                        name: "fk_consumption_records_food_base_food_id",
                        column: x => x.food_id,
                        principalTable: "food_base",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_consumption_records_user_meals_user_meal_id",
                        column: x => x.user_meal_id,
                        principalTable: "user_meals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_consumption_records_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_consumption_records_food_id",
                table: "consumption_records",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_records_user_id",
                table: "consumption_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_consumption_records_user_meal_id",
                table: "consumption_records",
                column: "user_meal_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumption_records");
        }
    }
}
