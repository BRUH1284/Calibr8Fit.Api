using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserProfileChangeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_profiles_users_user_id",
                table: "user_profiles");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "user_profiles",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_profiles_asp_net_users_id",
                table: "user_profiles",
                column: "id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_profiles_asp_net_users_id",
                table: "user_profiles");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_profiles",
                newName: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_profiles_users_user_id",
                table: "user_profiles",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
