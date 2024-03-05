using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Insight.Migrations
{
    public partial class ChangeFovoriteTeamAndPlayerColumnsType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoritePlayer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteTeam",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FavoritePlayerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavoriteTeamId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoritePlayerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteTeamId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FavoritePlayer",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavoriteTeam",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
