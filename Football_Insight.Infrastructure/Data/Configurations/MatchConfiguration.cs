using Football_Insight.Infrastructure.Data.Enums;
using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Infrastructure.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasData(
                new Match
                {
                    Id = 1,
                    DateAndTime = new DateTime(2023, 11, 1, 20, 00, 0),
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    StadiumId = 1,
                    HomeScore = 2,
                    AwayScore = 1,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 2,
                    DateAndTime = new DateTime(2023, 11, 10, 18, 00, 0),
                    HomeTeamId = 1,
                    AwayTeamId = 3,
                    StadiumId = 1,
                    HomeScore = 3,
                    AwayScore = 3,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 3,
                    DateAndTime = new DateTime(2023, 11, 20, 20, 30, 0),
                    HomeTeamId = 4,
                    AwayTeamId = 1,
                    StadiumId = 4,
                    HomeScore = 1,
                    AwayScore = 2,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 4,
                    DateAndTime = new DateTime(2023, 11, 5, 21, 00, 0),
                    HomeTeamId = 2,
                    AwayTeamId = 3,
                    StadiumId = 2,
                    HomeScore = 1,
                    AwayScore = 1,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 5,
                    DateAndTime = new DateTime(2023, 11, 15, 19, 45, 0),
                    HomeTeamId = 5,
                    AwayTeamId = 2,
                    StadiumId = 5,
                    HomeScore = 0,
                    AwayScore = 3,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 6,
                    DateAndTime = new DateTime(2023, 11, 25, 17, 00, 0),
                    HomeTeamId = 2,
                    AwayTeamId = 6,
                    StadiumId = 2,
                    HomeScore = 2,
                    AwayScore = 2,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 7,
                    DateAndTime = new DateTime(2023, 11, 5, 19, 30, 0),
                    HomeTeamId = 3,
                    AwayTeamId = 6,
                    StadiumId = 3,
                    HomeScore = 4,
                    AwayScore = 1,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 8,
                    DateAndTime = new DateTime(2023, 11, 30, 20, 00, 0),
                    HomeTeamId = 7,
                    AwayTeamId = 3,
                    StadiumId = 7,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 9,
                    DateAndTime = new DateTime(2023, 12, 10, 21, 00, 0),
                    HomeTeamId = 8,
                    AwayTeamId = 3,
                    StadiumId = 8,
                    HomeScore = 1,
                    AwayScore = 2,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 10,
                    DateAndTime = new DateTime(2023, 12, 15, 18, 00, 0),
                    HomeTeamId = 9,
                    AwayTeamId = 4,
                    StadiumId = 9,
                    HomeScore = 4,
                    AwayScore = 1,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 11,
                    DateAndTime = new DateTime(2023, 12, 20, 20, 00, 0),
                    HomeTeamId = 10,
                    AwayTeamId = 4,
                    StadiumId = 10,
                    HomeScore = 2,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 12,
                    DateAndTime = new DateTime(2023, 12, 25, 17, 30, 0),
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                    StadiumId = 4,
                    HomeScore = 3,
                    AwayScore = 4,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 13,
                    DateAndTime = new DateTime(2024, 1, 5, 19, 30, 0),
                    HomeTeamId = 5,
                    AwayTeamId = 6,
                    StadiumId = 5,
                    HomeScore = 2,
                    AwayScore = 2,
                    LeagueId = 1,
                    Status = MatchStatus.Finished,
                    Minutes = 90
                },
                new Match
                {
                    Id = 14,
                    DateAndTime = new DateTime(2024, 4, 27, 20, 00, 0),
                    HomeTeamId = 1,
                    AwayTeamId = 5,
                    StadiumId = 1,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 15,
                    DateAndTime = new DateTime(2024, 4, 25, 20, 30, 0),
                    HomeTeamId = 3,
                    AwayTeamId = 7,
                    StadiumId = 3,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 16,
                    DateAndTime = new DateTime(2024, 4, 30, 18, 00, 0),
                    HomeTeamId = 4,
                    AwayTeamId = 6,
                    StadiumId = 4,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 17,
                    DateAndTime = new DateTime(2024, 4, 25, 17, 30, 0),
                    HomeTeamId = 8,
                    AwayTeamId = 10,
                    StadiumId = 8,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 18,
                    DateAndTime = new DateTime(2024, 5, 2, 21, 00, 0),
                    HomeTeamId = 2,
                    AwayTeamId = 8,
                    StadiumId = 2,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 19,
                    DateAndTime = new DateTime(2024, 5, 10, 19, 45, 0),
                    HomeTeamId = 5,
                    AwayTeamId = 3,
                    StadiumId = 5,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 20,
                    DateAndTime = new DateTime(2024, 5, 15, 20, 00, 0),
                    HomeTeamId = 9,
                    AwayTeamId = 1,
                    StadiumId = 9,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                },
                new Match
                {
                    Id = 21,
                    DateAndTime = new DateTime(2024, 5, 23, 18, 30, 0),
                    HomeTeamId = 7,
                    AwayTeamId = 4,
                    StadiumId = 7,
                    HomeScore = 0,
                    AwayScore = 0,
                    LeagueId = 1,
                    Status = MatchStatus.Scheduled,
                    Minutes = 0
                }
            );
        }
    }
}
