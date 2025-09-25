using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserFollower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_followers",
                columns: table => new
                {
                    follower_id = table.Column<string>(type: "text", nullable: false),
                    followee_id = table.Column<string>(type: "text", nullable: false),
                    followed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_followers", x => new { x.follower_id, x.followee_id });
                    table.ForeignKey(
                        name: "fk_user_followers_asp_net_users_followee_id",
                        column: x => x.followee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_followers_asp_net_users_follower_id",
                        column: x => x.follower_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_followers_followee_id",
                table: "user_followers",
                column: "followee_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_followers_follower_id",
                table: "user_followers",
                column: "follower_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_followers");
        }
    }
}
