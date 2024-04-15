using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Football_Insight.Infrastructure.Migrations
{
    public partial class FullMigration : Migration
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "467b0785-ccda-40da-90e2-882be977d881", "1c063df5-31b3-4128-b1d5-b6a9af9a7993", "Admin", "ADMIN" },
                    { "7b012121-5960-4dff-bcd4-7ee24857279a", "12db278e-a6f8-4ede-b5ee-57fbf7ac8ec5", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "FavoritePlayerId", "FavoriteTeamId", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoPath", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6b108b67-01e2-4e45-8527-35ce18296203", 0, "", "062128eb-715b-42e0-8e6a-c65356cf862f", "", "user@fi.com", true, null, null, "", "", false, null, "USER@FI.COM", "USER@FI.COM", "AQAAAAEAACcQAAAAEA8Bc7TubZN215O/abzZHtpBk7S9EIzps+Y2e2hkbWuf9lhrFGSWyTDbOOTge1el1A==", null, false, "", "0523d7f5-d03c-4bd1-89a4-1b0d45bd2361", false, "user@fi.com" },
                    { "decc5187-d745-4493-9dc8-9bf8907f4942", 0, "", "3b964e35-6543-4293-bc82-b247c95f83f6", "", "admin@fi.com", true, null, null, "", "", false, null, "ADMIN@FI.COM", "ADMIN@FI.COM", "AQAAAAEAACcQAAAAELbcPSdQDxZilAnKr+gxROfhi5tABWv/q/zgqoYs+inf6RMewjtBkfkurYfK9b9U0w==", null, false, "", "9eea96dd-668d-4d4e-8a5b-3409d8d07c92", false, "admin@fi.com" }
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Super League" },
                    { 2, "Mystic League" }
                });

            migrationBuilder.InsertData(
                table: "Stadiums",
                columns: new[] { "Id", "Address", "Capacity", "Name", "YearBuilt" },
                values: new object[,]
                {
                    { 1, "Sir Matt Busby Way, Manchester", 74879, "Old Trafford", 1910 },
                    { 2, "Av. de Concha Espina, 1, Madrid", 81044, "Santiago Bernabéu", 1947 },
                    { 3, "Werner-Heisenberg-Allee 25, Munich", 75000, "Allianz Arena", 2005 },
                    { 4, "Piazzale Angelo Moratti, Milan", 80018, "San Siro", 1926 },
                    { 5, "Rue Henri Delaunay, Saint-Denis, Paris", 81338, "Stade de France", 1998 },
                    { 6, "Fulham Road, Fulham, London SW6 1HS", 40343, "Stamford Bridge", 1935 },
                    { 7, "C. d'Aristides Maillol, 12, Barcelona", 99354, "Camp Nou", 1957 },
                    { 8, "Strobelallee 50, 44139 Dortmund, North Rhine-Westphalia, Germany", 81365, "Signal Iduna Park", 1974 },
                    { 9, "Highbury House, 75 Drayton Park, London", 60704, "Emirates Stadium", 2006 },
                    { 10, "Anfield Rd, Liverpool", 53394, "Anfield", 1884 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7b012121-5960-4dff-bcd4-7ee24857279a", "6b108b67-01e2-4e45-8527-35ce18296203" },
                    { "467b0785-ccda-40da-90e2-882be977d881", "decc5187-d745-4493-9dc8-9bf8907f4942" },
                    { "7b012121-5960-4dff-bcd4-7ee24857279a", "decc5187-d745-4493-9dc8-9bf8907f4942" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Coach", "Founded", "LeagueId", "LogoURL", "Name", "StadiumId" },
                values: new object[,]
                {
                    { 1, "Ten Hag", 1878, 1, "https://static.flashscore.com/res/image/data/nwSRlyWg-h2pPXz3k.png", "Manchester United", 1 },
                    { 2, "Anceloti", 1902, 1, "https://static.flashscore.com/res/image/data/A7kHoxZA-fcDVLdrL.png", "Real Madrid", 2 },
                    { 3, "Thomas Tuchel", 1900, 1, "https://static.flashscore.com/res/image/data/tMir8iCr-xQOIafxi.png", "FC Bayern Munich", 3 },
                    { 4, "Sari", 1899, 1, "https://static.flashscore.com/res/image/data/htYjIjFa-fL3dmxxd.png", "AC Milan", 4 },
                    { 5, "Luis Enrique", 1970, 1, "https://static.flashscore.com/res/image/data/EskJufg5-06Ua3sOf.png", "Paris Saint-Germain", 5 },
                    { 6, "Pochetino", 1905, 1, "https://static.flashscore.com/res/image/data/GMmvDEdM-IROrZEJb.png", "Chelsea FC", 6 },
                    { 7, "Xavi", 1899, 1, "https://static.flashscore.com/res/image/data/8dhw5vxS-fcDVLdrL.png", "FC Barcelona", 7 },
                    { 8, "Edin Terzić", 1909, 1, "https://static.flashscore.com/res/image/data/Yiq1eU9r-WrjrBuU2.png", "Borussia Dortmund", 8 },
                    { 9, "Mikel Arteta", 1886, 1, "https://static.flashscore.com/res/image/data/pfchdCg5-vcNAdtF9.png", "Arsenal", 9 },
                    { 10, "Jurgen Klopp", 1950, 1, "https://static.flashscore.com/res/image/data/vwC9RGhl-Imx2oQd8.png", "Liverpool", 10 }
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "AwayScore", "AwayTeamId", "DateAndTime", "HomeScore", "HomeTeamId", "LeagueId", "Minutes", "StadiumId", "Status" },
                values: new object[,]
                {
                    { 1, 1, 2, new DateTime(2023, 11, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 1, 90, 1, 4 },
                    { 2, 3, 3, new DateTime(2023, 11, 10, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 1, 90, 1, 4 },
                    { 3, 2, 1, new DateTime(2023, 11, 20, 20, 30, 0, 0, DateTimeKind.Unspecified), 1, 4, 1, 90, 4, 4 },
                    { 4, 1, 3, new DateTime(2023, 11, 5, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 1, 90, 2, 4 },
                    { 5, 3, 2, new DateTime(2023, 11, 15, 19, 45, 0, 0, DateTimeKind.Unspecified), 0, 5, 1, 90, 5, 4 },
                    { 6, 2, 6, new DateTime(2023, 11, 25, 17, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 1, 90, 2, 4 },
                    { 7, 1, 6, new DateTime(2023, 11, 5, 19, 30, 0, 0, DateTimeKind.Unspecified), 4, 3, 1, 90, 3, 4 },
                    { 8, 0, 3, new DateTime(2023, 11, 30, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 7, 1, 90, 7, 4 },
                    { 9, 2, 3, new DateTime(2023, 12, 10, 21, 0, 0, 0, DateTimeKind.Unspecified), 1, 8, 1, 90, 8, 4 },
                    { 10, 1, 4, new DateTime(2023, 12, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), 4, 9, 1, 90, 9, 4 },
                    { 11, 0, 4, new DateTime(2023, 12, 20, 20, 0, 0, 0, DateTimeKind.Unspecified), 2, 10, 1, 90, 10, 4 },
                    { 12, 4, 5, new DateTime(2023, 12, 25, 17, 30, 0, 0, DateTimeKind.Unspecified), 3, 4, 1, 90, 4, 4 },
                    { 13, 2, 6, new DateTime(2024, 1, 5, 19, 30, 0, 0, DateTimeKind.Unspecified), 2, 5, 1, 90, 5, 4 },
                    { 14, 0, 5, new DateTime(2024, 4, 27, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 1, 1, 0, 1, 0 },
                    { 15, 0, 7, new DateTime(2024, 4, 25, 20, 30, 0, 0, DateTimeKind.Unspecified), 0, 3, 1, 0, 3, 0 },
                    { 16, 0, 6, new DateTime(2024, 4, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), 0, 4, 1, 0, 4, 0 },
                    { 17, 0, 10, new DateTime(2024, 4, 25, 17, 30, 0, 0, DateTimeKind.Unspecified), 0, 8, 1, 0, 8, 0 },
                    { 18, 0, 8, new DateTime(2024, 5, 2, 21, 0, 0, 0, DateTimeKind.Unspecified), 0, 2, 1, 0, 2, 0 },
                    { 19, 0, 3, new DateTime(2024, 5, 10, 19, 45, 0, 0, DateTimeKind.Unspecified), 0, 5, 1, 0, 5, 0 },
                    { 20, 0, 1, new DateTime(2024, 5, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 0, 9, 1, 0, 9, 0 },
                    { 21, 0, 4, new DateTime(2024, 5, 23, 18, 30, 0, 0, DateTimeKind.Unspecified), 0, 7, 1, 0, 7, 0 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Position", "Price", "Salary", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andre", "Onana", 0, 0m, 0m, 1 },
                    { 2, new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry", "Maguire", 1, 0m, 0m, 1 },
                    { 3, new DateTime(1994, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruno", "Fernandes", 2, 0m, 0m, 1 },
                    { 4, new DateTime(1997, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marcus", "Rashford", 3, 0m, 0m, 1 },
                    { 5, new DateTime(1992, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thibaut", "Courtois", 0, 0m, 0m, 2 },
                    { 6, new DateTime(1986, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Alaba", 1, 0m, 0m, 2 },
                    { 7, new DateTime(1985, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luka", "Modric", 2, 0m, 0m, 2 },
                    { 8, new DateTime(1987, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vinicius", "Junior", 3, 0m, 0m, 2 },
                    { 9, new DateTime(1986, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manuel", "Neuer", 0, 0m, 0m, 3 },
                    { 10, new DateTime(1996, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucas", "Hernandez", 1, 0m, 0m, 3 },
                    { 11, new DateTime(1995, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joshua", "Kimmich", 2, 0m, 0m, 3 },
                    { 12, new DateTime(1989, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thomas", "Muller", 3, 0m, 0m, 3 },
                    { 13, new DateTime(1995, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mike", "Maignan", 0, 0m, 0m, 4 },
                    { 14, new DateTime(1997, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fikayo", "Tomori", 1, 0m, 0m, 4 },
                    { 15, new DateTime(2000, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christian", "Pulisic", 2, 0m, 0m, 4 },
                    { 16, new DateTime(1986, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olivier", "Giroud", 3, 0m, 0m, 4 },
                    { 17, new DateTime(1999, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gianluigi", "Donnarumma", 0, 0m, 0m, 5 },
                    { 18, new DateTime(1994, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marquinhos", "Correa", 1, 0m, 0m, 5 },
                    { 19, new DateTime(1992, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marco", "Verratti", 2, 0m, 0m, 5 },
                    { 20, new DateTime(1998, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kylian", "Mbappe", 3, 0m, 0m, 5 },
                    { 21, new DateTime(1992, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert", "Sanchez", 0, 0m, 0m, 6 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName", "Position", "Price", "Salary", "TeamId" },
                values: new object[,]
                {
                    { 22, new DateTime(1984, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thiago", "Silva", 1, 0m, 0m, 6 },
                    { 23, new DateTime(1991, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cole", "Palmer", 2, 0m, 0m, 6 },
                    { 24, new DateTime(1999, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nicolas", "Jackson", 3, 0m, 0m, 6 },
                    { 25, new DateTime(1992, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marc-Andre", "ter Stegen", 0, 0m, 0m, 7 },
                    { 26, new DateTime(1987, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eric", "Garcia", 1, 0m, 0m, 7 },
                    { 27, new DateTime(1997, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frenkie", "de Jong", 2, 0m, 0m, 7 },
                    { 28, new DateTime(2002, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert", "Lewandovski", 3, 0m, 0m, 7 },
                    { 29, new DateTime(1997, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gregor", "Kobel", 0, 0m, 0m, 8 },
                    { 30, new DateTime(1988, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mats", "Hummels", 1, 0m, 0m, 8 },
                    { 31, new DateTime(2003, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marco", "Reus", 2, 0m, 0m, 8 },
                    { 32, new DateTime(1999, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Donyell", "Malen", 3, 0m, 0m, 8 },
                    { 33, new DateTime(2001, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bukayo", "Saka", 3, 0m, 0m, 9 },
                    { 34, new DateTime(1998, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martin", "Odegaard", 2, 0m, 0m, 9 },
                    { 35, new DateTime(1997, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel", "Magalhaes", 1, 0m, 0m, 9 },
                    { 36, new DateTime(1998, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aaron", "Ramsdale", 0, 0m, 0m, 9 },
                    { 37, new DateTime(1992, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alisson", "Becker", 0, 0m, 0m, 10 },
                    { 38, new DateTime(1991, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Virgil", "van Dijk", 1, 0m, 0m, 10 },
                    { 39, new DateTime(1991, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thiago", "Alcântara", 2, 0m, 0m, 10 },
                    { 40, new DateTime(1992, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohamed", "Salah", 3, 0m, 0m, 10 }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "GoalAssistantId", "GoalMinute", "GoalScorerId", "MatchId", "TeamId" },
                values: new object[,]
                {
                    { 1, 3, 35, 4, 1, 1 },
                    { 2, 4, 78, 3, 1, 1 },
                    { 3, 7, 52, 8, 1, 2 },
                    { 4, 3, 15, 4, 2, 1 },
                    { 5, 2, 47, 3, 2, 1 },
                    { 6, 4, 78, 2, 2, 1 },
                    { 7, 11, 29, 12, 2, 3 },
                    { 8, 10, 55, 11, 2, 3 },
                    { 9, 12, 85, 10, 2, 3 },
                    { 10, 3, 22, 4, 3, 1 },
                    { 11, null, 68, 3, 3, 1 },
                    { 12, 15, 51, 16, 3, 4 },
                    { 13, 7, 36, 8, 4, 2 },
                    { 14, 11, 78, 12, 4, 3 },
                    { 15, 7, 12, 8, 5, 2 },
                    { 16, 8, 45, 6, 5, 2 },
                    { 17, null, 73, 7, 5, 2 },
                    { 18, 7, 24, 8, 6, 2 },
                    { 19, 8, 54, 6, 6, 2 },
                    { 20, 23, 38, 24, 6, 6 },
                    { 21, 23, 76, 22, 6, 6 },
                    { 22, 11, 12, 12, 7, 3 },
                    { 23, 10, 28, 11, 7, 3 },
                    { 24, 12, 53, 10, 7, 3 },
                    { 25, null, 79, 9, 7, 3 },
                    { 26, 23, 65, 24, 7, 6 },
                    { 27, 11, 18, 12, 9, 3 },
                    { 28, 10, 57, 11, 9, 3 },
                    { 29, 31, 73, 32, 9, 8 },
                    { 30, 34, 11, 33, 10, 9 },
                    { 31, 35, 27, 34, 10, 9 },
                    { 32, null, 49, 33, 10, 9 },
                    { 33, 33, 68, 34, 10, 9 },
                    { 34, 15, 78, 16, 10, 4 },
                    { 35, 39, 32, 40, 11, 10 },
                    { 36, 40, 71, 38, 11, 10 },
                    { 37, 15, 11, 16, 12, 4 },
                    { 38, 16, 34, 14, 12, 4 },
                    { 39, 14, 78, 15, 12, 4 },
                    { 40, 19, 22, 20, 12, 5 },
                    { 41, 20, 45, 18, 12, 5 },
                    { 42, null, 60, 20, 12, 5 }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "GoalAssistantId", "GoalMinute", "GoalScorerId", "MatchId", "TeamId" },
                values: new object[,]
                {
                    { 43, null, 85, 17, 12, 5 },
                    { 44, 19, 18, 20, 13, 5 },
                    { 45, 20, 52, 18, 13, 5 },
                    { 46, 23, 37, 24, 13, 6 },
                    { 47, 24, 76, 22, 13, 6 }
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
