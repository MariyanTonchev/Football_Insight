using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Insight.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Optional. The user's first name. This is not required at registration and can be filled in later."),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "Optional. The user's last name. This is not required at registration and can be filled in later."),
                    FavoriteTeamId = table.Column<int>(type: "int", nullable: true, comment: "Optional. ID of the user's favorite team. This is not required at registration and can be filled in later."),
                    FavoritePlayerId = table.Column<int>(type: "int", nullable: true, comment: "Optional. ID of the user's favorite player. This is not required at registration and can be filled in later."),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Optional. The user's country. This is not required at registration and can be filled in later."),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Optional. The user's city. This is not required at registration and can be filled in later."),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Optional. Path to the user's photo. This can be updated to enhance the user's profile."),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                },
                comment: "Represents the user profile within the application, extending the IdentityUser with custom properties for a personalized experience.");

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier for the league.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false, comment: "The name of the league.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                },
                comment: "Represent information about league.");

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier for the stadium.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the stadium."),
                    Address = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false, comment: "The physical address of the stadium."),
                    Capacity = table.Column<int>(type: "int", nullable: false, comment: "The seating capacity of the stadium."),
                    YearBuilt = table.Column<int>(type: "int", nullable: false, comment: "The year when the stadium was built.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.Id);
                },
                comment: "Represent information about the stadium.");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier for the team.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the team."),
                    Founded = table.Column<int>(type: "int", nullable: false, comment: "The year the team was founded."),
                    LogoURL = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "URL to the team logo image."),
                    Coach = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the team current coach."),
                    LeagueId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to the League entity. Indicates the league in which the team competes."),
                    StadiumId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to the Stadium entity. Represents the home stadium of the team.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represent information about the team.");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier for the match.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the match is scheduled to take place."),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key representing the home team. References the Team entity."),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key representing the away team. References the Team entity."),
                    StadiumId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key for the stadium where the match is held. References the Stadium entity."),
                    HomeScore = table.Column<int>(type: "int", nullable: false, comment: "The score of the home team at the end of the match."),
                    AwayScore = table.Column<int>(type: "int", nullable: false, comment: "The score of the away team at the end of the match."),
                    LeagueId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key for the league to which this match belongs. References the League entity."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "The current status of the match."),
                    Minutes = table.Column<int>(type: "int", nullable: false, comment: "The total minutes played in the match.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Stadiums_StadiumId",
                        column: x => x.StadiumId,
                        principalTable: "Stadiums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                },
                comment: "Represent information about the match.");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier for the player.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The player's first name."),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The player's last name."),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date of birth of the player."),
                    Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, comment: "The transfer or market price of the player."),
                    Salary = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, comment: "The salary of the player."),
                    TeamId = table.Column<int>(type: "int", nullable: false, comment: "Foreign key to the Team entity. Represents the team to which the player currently belongs."),
                    Position = table.Column<int>(type: "int", nullable: false, comment: "The position of the player on the field.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represent information about the player.");

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The ID of the user who has marked a match as a favorite."),
                    MatchId = table.Column<int>(type: "int", nullable: false, comment: "The ID of the match that has been marked as favorite by the user.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => new { x.UserId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_Favorite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Represent a relationship between a user and their favorite matches.");

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Unique identifier for the goal.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the match in which the goal was scored. Foreign key that references the Match entity."),
                    TeamId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the team that scored the goal. Foreign key that references the Team entity."),
                    GoalScorerId = table.Column<int>(type: "int", nullable: false, comment: "Identifier of the player who scored the goal. Foreign key that references the Player entity as the goal scorer."),
                    GoalAssistantId = table.Column<int>(type: "int", nullable: true, comment: "Optional identifier of the player who assisted the goal. Can be null if there was no assist."),
                    GoalMinute = table.Column<int>(type: "int", nullable: false, comment: "The minute of the match in which the goal was scored.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_Players_GoalAssistantId",
                        column: x => x.GoalAssistantId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Players_GoalScorerId",
                        column: x => x.GoalScorerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                },
                comment: "Represent information about goals scored in matches.");

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
                columns: new[] { "Id", "Coach", "Founded", "LeagueId", "LogoURL", "Name", "StadiumId" },
                values: new object[,]
                {
                    { 1, "Sir Alex", 1878, 1, "", "Manchester United", 1 },
                    { 2, "Anceloti", 1902, 2, "", "Real Madrid", 2 },
                    { 3, "Thomas Tuchel", 1900, 3, "", "FC Bayern Munich", 3 },
                    { 4, "Sari", 1899, 4, "", "AC Milan", 4 },
                    { 5, "Luis Enrique", 1970, 5, "", "Paris Saint-Germain", 5 },
                    { 6, "Pochetino", 1905, 1, "", "Chelsea FC", 6 },
                    { 7, "Xavi", 1899, 2, "", "FC Barcelona", 7 },
                    { 8, "George", 1909, 3, "", "Borussia Dortmund", 2 },
                    { 9, "Simeone Indzhagi", 1908, 4, "", "Inter Milan", 4 },
                    { 10, "No Idea", 1950, 5, "", "Olympique Lyonnais", 3 },
                    { 11, "Mikel Arteta", 1886, 1, "", "Arsenal", 1 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Position", "Price", "Salary", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry", "Maguire", 1, 0m, 0m, 1 },
                    { 2, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marcus", "Rashford", 3, 0m, 0m, 1 },
                    { 3, new DateTime(1995, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andre", "Onana", 0, 0m, 0m, 1 },
                    { 31, new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bukayo", "Saka", 3, 0m, 0m, 11 },
                    { 32, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "William", "Salliba", 1, 0m, 0m, 11 },
                    { 33, new DateTime(1998, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ben", "White", 1, 0m, 0m, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_MatchId",
                table: "Favorite",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_GoalAssistantId",
                table: "Goals",
                column: "GoalAssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_GoalScorerId",
                table: "Goals",
                column: "GoalScorerId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_MatchId",
                table: "Goals",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_TeamId",
                table: "Goals",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayTeamId",
                table: "Matches",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_StadiumId",
                table: "Matches",
                column: "StadiumId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_StadiumId",
                table: "Teams",
                column: "StadiumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Stadiums");
        }
    }
}
