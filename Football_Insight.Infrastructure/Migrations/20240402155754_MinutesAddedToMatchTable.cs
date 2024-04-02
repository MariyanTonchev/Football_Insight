using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Insight.Infrastructure.Migrations
{
    public partial class MinutesAddedToMatchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Minutes",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "Matches");
        }
    }
}
