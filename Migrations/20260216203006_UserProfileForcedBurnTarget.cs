using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileForcedBurnTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "forced_burn_target",
                table: "user_profiles",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forced_burn_target",
                table: "user_profiles");
        }
    }
}
