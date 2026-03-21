using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wepAPI_denemeler.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavGames_Games_GameId",
                table: "FavGames");

            migrationBuilder.DropForeignKey(
                name: "FK_FavGames_Users_UserId",
                table: "FavGames");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAds_Games_GameId",
                table: "GameAds");

            migrationBuilder.DropForeignKey(
                name: "FK_GameAds_Users_UserId",
                table: "GameAds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameAds",
                table: "GameAds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavGames",
                table: "FavGames");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "games");

            migrationBuilder.RenameTable(
                name: "GameAds",
                newName: "gameads");

            migrationBuilder.RenameTable(
                name: "FavGames",
                newName: "favgames");

            migrationBuilder.RenameIndex(
                name: "IX_GameAds_UserId",
                table: "gameads",
                newName: "IX_gameads_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameAds_GameId",
                table: "gameads",
                newName: "IX_gameads_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_FavGames_UserId",
                table: "favgames",
                newName: "IX_favgames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavGames_GameId",
                table: "favgames",
                newName: "IX_favgames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_games",
                table: "games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gameads",
                table: "gameads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favgames",
                table: "favgames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_favgames_games_GameId",
                table: "favgames",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favgames_users_UserId",
                table: "favgames",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gameads_games_GameId",
                table: "gameads",
                column: "GameId",
                principalTable: "games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_gameads_users_UserId",
                table: "gameads",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favgames_games_GameId",
                table: "favgames");

            migrationBuilder.DropForeignKey(
                name: "FK_favgames_users_UserId",
                table: "favgames");

            migrationBuilder.DropForeignKey(
                name: "FK_gameads_games_GameId",
                table: "gameads");

            migrationBuilder.DropForeignKey(
                name: "FK_gameads_users_UserId",
                table: "gameads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_games",
                table: "games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gameads",
                table: "gameads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favgames",
                table: "favgames");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "games",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "gameads",
                newName: "GameAds");

            migrationBuilder.RenameTable(
                name: "favgames",
                newName: "FavGames");

            migrationBuilder.RenameIndex(
                name: "IX_gameads_UserId",
                table: "GameAds",
                newName: "IX_GameAds_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_gameads_GameId",
                table: "GameAds",
                newName: "IX_GameAds_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_favgames_UserId",
                table: "FavGames",
                newName: "IX_FavGames_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_favgames_GameId",
                table: "FavGames",
                newName: "IX_FavGames_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameAds",
                table: "GameAds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavGames",
                table: "FavGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavGames_Games_GameId",
                table: "FavGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavGames_Users_UserId",
                table: "FavGames",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAds_Games_GameId",
                table: "GameAds",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameAds_Users_UserId",
                table: "GameAds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
