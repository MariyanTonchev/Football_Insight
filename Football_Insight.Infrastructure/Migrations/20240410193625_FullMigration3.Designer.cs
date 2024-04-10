﻿// <auto-generated />
using System;
using Football_Insight.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Football_Insight.Infrastructure.Migrations
{
    [DbContext(typeof(FootballInsightDbContext))]
    [Migration("20240410193625_FullMigration3")]
    partial class FullMigration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("FavoritePlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("FavoriteTeamId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Favorite", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GoalAssistantId")
                        .HasColumnType("int");

                    b.Property<int>("GoalMinute")
                        .HasColumnType("int");

                    b.Property<int>("GoalScorerId")
                        .HasColumnType("int");

                    b.Property<int>("MatchId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GoalAssistantId");

                    b.HasIndex("GoalScorerId");

                    b.HasIndex("MatchId");

                    b.HasIndex("TeamId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Premier League"
                        },
                        new
                        {
                            Id = 2,
                            Name = "La Liga"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bundesliga"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Serie A"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Ligue 1"
                        });
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AwayScore")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("HomeScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<int>("Minutes")
                        .HasColumnType("int");

                    b.Property<int>("StadiumId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("LeagueId");

                    b.HasIndex("StadiumId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<decimal>("Salary")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1993, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Harry",
                            LastName = "Maguire",
                            Position = 1,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Marcus",
                            LastName = "Rashford",
                            Position = 3,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1995, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Andre",
                            LastName = "Onana",
                            Position = 0,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 1
                        },
                        new
                        {
                            Id = 31,
                            DateOfBirth = new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Bukayo",
                            LastName = "Saka",
                            Position = 3,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 11
                        },
                        new
                        {
                            Id = 32,
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "William",
                            LastName = "Salliba",
                            Position = 1,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 11
                        },
                        new
                        {
                            Id = 33,
                            DateOfBirth = new DateTime(1998, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Ben",
                            LastName = "White",
                            Position = 1,
                            Price = 0m,
                            Salary = 0m,
                            TeamId = 11
                        });
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Stadium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(96)
                        .HasColumnType("nvarchar(96)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stadiums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Highbury House, 75 Drayton Park, London",
                            Capacity = 60704,
                            Name = "Emirates Stadium",
                            YearBuilt = 2006
                        },
                        new
                        {
                            Id = 2,
                            Address = "Sir Matt Busby Way, Manchester",
                            Capacity = 74879,
                            Name = "Old Trafford",
                            YearBuilt = 1910
                        },
                        new
                        {
                            Id = 3,
                            Address = "C. d'Aristides Maillol, 12, Barcelona",
                            Capacity = 99354,
                            Name = "Camp Nou",
                            YearBuilt = 1957
                        },
                        new
                        {
                            Id = 4,
                            Address = "London, HA9 0WS",
                            Capacity = 90000,
                            Name = "Wembley Stadium",
                            YearBuilt = 2007
                        },
                        new
                        {
                            Id = 5,
                            Address = "Av. Pres. Castelo Branco, Rio de Janeiro",
                            Capacity = 78838,
                            Name = "Maracanã",
                            YearBuilt = 1950
                        },
                        new
                        {
                            Id = 6,
                            Address = "Piazzale Angelo Moratti, Milan",
                            Capacity = 80018,
                            Name = "San Siro",
                            YearBuilt = 1926
                        },
                        new
                        {
                            Id = 7,
                            Address = "Calz. de Tlalpan 3665, Mexico City",
                            Capacity = 87523,
                            Name = "Estadio Azteca",
                            YearBuilt = 1966
                        });
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Coach")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Founded")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("LogoURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StadiumId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("StadiumId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Coach = "Sir Alex",
                            Founded = 1878,
                            LeagueId = 1,
                            LogoURL = "",
                            Name = "Manchester United",
                            StadiumId = 1
                        },
                        new
                        {
                            Id = 2,
                            Coach = "Anceloti",
                            Founded = 1902,
                            LeagueId = 2,
                            LogoURL = "",
                            Name = "Real Madrid",
                            StadiumId = 2
                        },
                        new
                        {
                            Id = 3,
                            Coach = "Thomas Tuchel",
                            Founded = 1900,
                            LeagueId = 3,
                            LogoURL = "",
                            Name = "FC Bayern Munich",
                            StadiumId = 3
                        },
                        new
                        {
                            Id = 4,
                            Coach = "Sari",
                            Founded = 1899,
                            LeagueId = 4,
                            LogoURL = "",
                            Name = "AC Milan",
                            StadiumId = 4
                        },
                        new
                        {
                            Id = 5,
                            Coach = "Luis Enrique",
                            Founded = 1970,
                            LeagueId = 5,
                            LogoURL = "",
                            Name = "Paris Saint-Germain",
                            StadiumId = 5
                        },
                        new
                        {
                            Id = 6,
                            Coach = "Pochetino",
                            Founded = 1905,
                            LeagueId = 1,
                            LogoURL = "",
                            Name = "Chelsea FC",
                            StadiumId = 6
                        },
                        new
                        {
                            Id = 7,
                            Coach = "Xavi",
                            Founded = 1899,
                            LeagueId = 2,
                            LogoURL = "",
                            Name = "FC Barcelona",
                            StadiumId = 7
                        },
                        new
                        {
                            Id = 8,
                            Coach = "George",
                            Founded = 1909,
                            LeagueId = 3,
                            LogoURL = "",
                            Name = "Borussia Dortmund",
                            StadiumId = 2
                        },
                        new
                        {
                            Id = 9,
                            Coach = "Simeone Indzhagi",
                            Founded = 1908,
                            LeagueId = 4,
                            LogoURL = "",
                            Name = "Inter Milan",
                            StadiumId = 4
                        },
                        new
                        {
                            Id = 10,
                            Coach = "No Idea",
                            Founded = 1950,
                            LeagueId = 5,
                            LogoURL = "",
                            Name = "Olympique Lyonnais",
                            StadiumId = 3
                        },
                        new
                        {
                            Id = 11,
                            Coach = "Mikel Arteta",
                            Founded = 1886,
                            LeagueId = 1,
                            LogoURL = "",
                            Name = "Arsenal",
                            StadiumId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Favorite", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Match", "Match")
                        .WithMany("Favorites")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.ApplicationUser", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Goal", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Player", "GoalAssistant")
                        .WithMany("GoalsAssisted")
                        .HasForeignKey("GoalAssistantId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Player", "GoalScorer")
                        .WithMany("GoalsScored")
                        .HasForeignKey("GoalScorerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Team", "Team")
                        .WithMany("Goals")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GoalAssistant");

                    b.Navigation("GoalScorer");

                    b.Navigation("Match");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Match", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.League", "League")
                        .WithMany("Match")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Stadium", "Stadium")
                        .WithMany("Matches")
                        .HasForeignKey("StadiumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeTeam");

                    b.Navigation("League");

                    b.Navigation("Stadium");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Player", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Team", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.Stadium", "Stadium")
                        .WithMany("Teams")
                        .HasForeignKey("StadiumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");

                    b.Navigation("Stadium");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Football_Insight.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Football_Insight.Infrastructure.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.League", b =>
                {
                    b.Navigation("Match");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Match", b =>
                {
                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Player", b =>
                {
                    b.Navigation("GoalsAssisted");

                    b.Navigation("GoalsScored");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Stadium", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Football_Insight.Infrastructure.Data.Models.Team", b =>
                {
                    b.Navigation("AwayMatches");

                    b.Navigation("Goals");

                    b.Navigation("HomeMatches");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
