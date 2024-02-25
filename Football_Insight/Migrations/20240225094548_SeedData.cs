using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Insight.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Premier League" },
                    { 2, "La Liga" },
                    { 3, "Bundesliga" },
                    { 4, "Serie A" },
                    { 5, "Ligue 1" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Goalkeeper" },
                    { 2, "Defender" },
                    { 3, "Midfielder" },
                    { 4, "Forward" }
                });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "Address", "Capacity", "Name", "YearBuilt" },
                values: new object[,]
                {
                    { 1, "Highbury House, 75 Drayton Park, London", 60704, "Emirates Stadium", 2006 },
                    { 2, "Sir Matt Busby Way, Manchester", 74879, "Old Trafford", 1910 },
                    { 3, "C. d'Aristides Maillol, 12, Barcelona", 99354, "Camp Nou", 1957 },
                    { 4, "London, HA9 0WS", 90000, "Wembley Stadium", 2007 },
                    { 5, "Av. Pres. Castelo Branco, Rio de Janeiro", 78838, "Maracanã", 1950 },
                    { 6, "Piazzale Angelo Moratti, Milan", 80018, "San Siro", 1926 },
                    { 7, "Calz. de Tlalpan 3665, Mexico City", 87523, "Estadio Azteca", 1966 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Founded", "LeagueId", "Name", "StadiumId" },
                values: new object[,]
                {
                    { 1, 1878, 1, "Manchester United", 1 },
                    { 2, 1902, 2, "Real Madrid", 2 },
                    { 3, 1900, 3, "FC Bayern Munich", 3 },
                    { 4, 1899, 4, "AC Milan", 4 },
                    { 5, 1970, 5, "Paris Saint-Germain", 5 },
                    { 6, 1905, 1, "Chelsea FC", 6 },
                    { 7, 1899, 2, "FC Barcelona", 7 },
                    { 8, 1909, 3, "Borussia Dortmund", 2 },
                    { 9, 1908, 4, "Inter Milan", 4 },
                    { 10, 1950, 5, "Olympique Lyonnais", 3 },
                    { 11, 1886, 1, "Arsenal", 1 }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Salary", "TeamId", "Trophies" },
                values: new object[,]
                {
                    { 1, new DateTime(1971, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pep", "Guardiola", 20000m, 1, 30 },
                    { 2, new DateTime(1972, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zinedine", "Zidane", 19000m, 2, 11 },
                    { 3, new DateTime(1967, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurgen", "Klopp", 18000m, 3, 9 },
                    { 4, new DateTime(1959, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carlo", "Ancelotti", 16000m, 4, 20 },
                    { 5, new DateTime(1970, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diego", "Simeone", 14000m, 5, 8 },
                    { 6, new DateTime(1972, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauricio", "Pochettino", 12000m, 6, 2 },
                    { 7, new DateTime(1972, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauricio", "Pochettino", 12000m, 7, 2 },
                    { 8, new DateTime(1973, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thomas", "Tuchel", 16000m, 8, 6 },
                    { 9, new DateTime(1972, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauricio", "Pochettino", 12000m, 9, 2 },
                    { 10, new DateTime(1972, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mauricio", "Pochettino", 12000m, 10, 2 },
                    { 11, new DateTime(1983, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mikel", "Arteta", 16000m, 11, 4 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "PositionId", "Price", "Salary", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry", "Maguire", 2, 0m, 0m, 1 },
                    { 2, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marcus", "Rashford", 4, 0m, 0m, 1 },
                    { 3, new DateTime(1995, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andre", "Onana", 1, 0m, 0m, 1 },
                    { 31, new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bukayo", "Saka", 4, 0m, 0m, 11 },
                    { 32, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William", "Salliba", 2, 0m, 0m, 11 },
                    { 33, new DateTime(1998, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", "White", 2, 0m, 0m, 11 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stadiums",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
