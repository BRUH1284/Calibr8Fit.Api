using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calibr8Fit.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFriendships_FriendRequests_ProfilePictires_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_picture_file_name",
                table: "user_profiles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "friend_requests",
                columns: table => new
                {
                    requester_id = table.Column<string>(type: "text", nullable: false),
                    addressee_id = table.Column<string>(type: "text", nullable: false),
                    requested_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friend_requests", x => new { x.requester_id, x.addressee_id });
                    table.ForeignKey(
                        name: "fk_friend_requests_asp_net_users_addressee_id",
                        column: x => x.addressee_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_friend_requests_asp_net_users_requester_id",
                        column: x => x.requester_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friendships",
                columns: table => new
                {
                    user_a_id = table.Column<string>(type: "text", nullable: false),
                    user_b_id = table.Column<string>(type: "text", nullable: false),
                    friends_since = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_friendships", x => new { x.user_a_id, x.user_b_id });
                    table.ForeignKey(
                        name: "fk_friendships_users_user_a_id",
                        column: x => x.user_a_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_friendships_users_user_b_id",
                        column: x => x.user_b_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profile_pictures",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_profile_pictures", x => new { x.user_id, x.file_name });
                    table.ForeignKey(
                        name: "fk_profile_pictures_user_profiles_user_id",
                        column: x => x.user_id,
                        principalTable: "user_profiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_food_base_user_id",
                table: "food_base",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_base_activities_user_id",
                table: "base_activities",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_addressee_id",
                table: "friend_requests",
                column: "addressee_id");

            migrationBuilder.CreateIndex(
                name: "ix_friend_requests_requester_id",
                table: "friend_requests",
                column: "requester_id");

            migrationBuilder.CreateIndex(
                name: "ix_friendships_user_a_id",
                table: "friendships",
                column: "user_a_id");

            migrationBuilder.CreateIndex(
                name: "ix_friendships_user_b_id",
                table: "friendships",
                column: "user_b_id");

            migrationBuilder.CreateIndex(
                name: "ix_profile_pictures_user_id",
                table: "profile_pictures",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friend_requests");

            migrationBuilder.DropTable(
                name: "friendships");

            migrationBuilder.DropTable(
                name: "profile_pictures");

            migrationBuilder.DropIndex(
                name: "ix_food_base_user_id",
                table: "food_base");

            migrationBuilder.DropIndex(
                name: "ix_base_activities_user_id",
                table: "base_activities");

            migrationBuilder.DropColumn(
                name: "profile_picture_file_name",
                table: "user_profiles");
        }
    }
}
