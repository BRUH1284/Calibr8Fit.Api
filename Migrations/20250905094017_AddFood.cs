using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    nutrition_density = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_foods", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "foods");
        }
    }
}
