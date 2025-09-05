using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFoodAndUserFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "food_base",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    caloric_value = table.Column<float>(type: "real", nullable: false),
                    fat = table.Column<float>(type: "real", nullable: false),
                    saturated_fats = table.Column<float>(type: "real", nullable: false),
                    monounsaturated_fats = table.Column<float>(type: "real", nullable: false),
                    polyunsaturated_fats = table.Column<float>(type: "real", nullable: false),
                    carbohydrates = table.Column<float>(type: "real", nullable: false),
                    sugars = table.Column<float>(type: "real", nullable: false),
                    protein = table.Column<float>(type: "real", nullable: false),
                    dietary_fiber = table.Column<float>(type: "real", nullable: false),
                    water = table.Column<float>(type: "real", nullable: false),
                    cholesterol = table.Column<float>(type: "real", nullable: false),
                    sodium = table.Column<float>(type: "real", nullable: false),
                    vitamin_a = table.Column<float>(type: "real", nullable: false),
                    vitamin_b1thiamine = table.Column<float>(type: "real", nullable: false),
                    vitamin_b11folic_acid = table.Column<float>(type: "real", nullable: false),
                    vitamin_b12 = table.Column<float>(type: "real", nullable: false),
                    vitamin_b2riboflavin = table.Column<float>(type: "real", nullable: false),
                    vitamin_b3niacin = table.Column<float>(type: "real", nullable: false),
                    vitamin_b5pantothenic_acid = table.Column<float>(type: "real", nullable: false),
                    vitamin_b6 = table.Column<float>(type: "real", nullable: false),
                    vitamin_c = table.Column<float>(type: "real", nullable: false),
                    vitamin_d = table.Column<float>(type: "real", nullable: false),
                    vitamin_e = table.Column<float>(type: "real", nullable: false),
                    vitamin_k = table.Column<float>(type: "real", nullable: false),
                    calcium = table.Column<float>(type: "real", nullable: false),
                    copper = table.Column<float>(type: "real", nullable: false),
                    iron = table.Column<float>(type: "real", nullable: false),
                    magnesium = table.Column<float>(type: "real", nullable: false),
                    manganese = table.Column<float>(type: "real", nullable: false),
                    phosphorus = table.Column<float>(type: "real", nullable: false),
                    potassium = table.Column<float>(type: "real", nullable: false),
                    selenium = table.Column<float>(type: "real", nullable: false),
                    zinc = table.Column<float>(type: "real", nullable: false),
                    nutrition_density = table.Column<float>(type: "real", nullable: false),
                    is_user_food = table.Column<bool>(type: "boolean", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    synced_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_food_base", x => x.id);
                    table.ForeignKey(
                        name: "fk_food_base_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_food_base_user_id_id",
                table: "food_base",
                columns: new[] { "user_id", "id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_base");
        }
    }
}
