using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Persistence.SocialNetworkDb.Migrations
{
    /// <inheritdoc />
    public partial class Renamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostLikeCount",
                schema: "socialNetwork",
                table: "Posts",
                newName: "LikeCount");

            migrationBuilder.RenameColumn(
                name: "CommentLikeCount",
                schema: "socialNetwork",
                table: "Comments",
                newName: "LikeCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeCount",
                schema: "socialNetwork",
                table: "Posts",
                newName: "PostLikeCount");

            migrationBuilder.RenameColumn(
                name: "LikeCount",
                schema: "socialNetwork",
                table: "Comments",
                newName: "CommentLikeCount");
        }
    }
}
