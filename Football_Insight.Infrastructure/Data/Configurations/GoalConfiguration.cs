using Football_Insight.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Football_Insight.Infrastructure.Data.Configurations
{
    public class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.HasData(
                new Goal
                {
                    Id = 1,
                    MatchId = 1,
                    TeamId = 1,
                    GoalScorerId = 4,
                    GoalAssistantId = 3,
                    GoalMinute = 35
                },
                new Goal
                {
                    Id = 2,
                    MatchId = 1,
                    TeamId = 1,
                    GoalScorerId = 3,
                    GoalAssistantId = 4,
                    GoalMinute = 78
                },
                new Goal
                {
                    Id = 3,
                    MatchId = 1,
                    TeamId = 2,
                    GoalScorerId = 8,
                    GoalAssistantId = 7,
                    GoalMinute = 52
                },
                new Goal
                {
                    Id = 4,
                    MatchId = 2,
                    TeamId = 1,
                    GoalScorerId = 4,
                    GoalAssistantId = 3,
                    GoalMinute = 15
                },
                                new Goal
                                {
                                    Id = 5,
                                    MatchId = 2,
                                    TeamId = 1,
                                    GoalScorerId = 3,
                                    GoalAssistantId = 2,
                                    GoalMinute = 47
                                },
                                new Goal
                                {
                                    Id = 6,
                                    MatchId = 2,
                                    TeamId = 1,
                                    GoalScorerId = 2,
                                    GoalAssistantId = 4,
                                    GoalMinute = 78
                                },
                                new Goal
                                {
                                    Id = 7,
                                    MatchId = 2,
                                    TeamId = 3,
                                    GoalScorerId = 12,
                                    GoalAssistantId = 11,
                                    GoalMinute = 29
                                },
                                new Goal
                                {
                                    Id = 8,
                                    MatchId = 2,
                                    TeamId = 3,
                                    GoalScorerId = 11,
                                    GoalAssistantId = 10,
                                    GoalMinute = 55
                                },
                                new Goal
                                {
                                    Id = 9,
                                    MatchId = 2,
                                    TeamId = 3,
                                    GoalScorerId = 10,
                                    GoalAssistantId = 12,
                                    GoalMinute = 85
                                },
                    new Goal
                    {
                        Id = 10,
                        MatchId = 3,
                        TeamId = 1,
                        GoalScorerId = 4,
                        GoalAssistantId = 3,
                        GoalMinute = 22
                    },
        new Goal
        {
            Id = 11,
            MatchId = 3,
            TeamId = 1,
            GoalScorerId = 3,
            GoalAssistantId = null,
            GoalMinute = 68
        },
        new Goal
        {
            Id = 12,
            MatchId = 3,
            TeamId = 4,
            GoalScorerId = 16,
            GoalAssistantId = 15,
            GoalMinute = 51
        },
        new Goal
        {
            Id = 13,
            MatchId = 4,
            TeamId = 2,
            GoalScorerId = 8,
            GoalAssistantId = 7,
            GoalMinute = 36
        },
        new Goal
        {
            Id = 14,
            MatchId = 4,
            TeamId = 3,
            GoalScorerId = 12,
            GoalAssistantId = 11,
            GoalMinute = 78
        },
        new Goal
        {
            Id = 15,
            MatchId = 5,
            TeamId = 2,
            GoalScorerId = 8,
            GoalAssistantId = 7,
            GoalMinute = 12
        },
        new Goal
        {
            Id = 16,
            MatchId = 5,
            TeamId = 2,
            GoalScorerId = 6,
            GoalAssistantId = 8,
            GoalMinute = 45
        },
        new Goal
        {
            Id = 17,
            MatchId = 5,
            TeamId = 2,
            GoalScorerId = 7,
            GoalAssistantId = null,
            GoalMinute = 73
        },
        new Goal
        {
            Id = 18,
            MatchId = 6,
            TeamId = 2,
            GoalScorerId = 8,
            GoalAssistantId = 7,
            GoalMinute = 24
        },
        new Goal
        {
            Id = 19,
            MatchId = 6,
            TeamId = 2,
            GoalScorerId = 6,
            GoalAssistantId = 8,
            GoalMinute = 54
        },
        new Goal
        {
            Id = 20,
            MatchId = 6,
            TeamId = 6,
            GoalScorerId = 24,
            GoalAssistantId = 23,
            GoalMinute = 38
        },
        new Goal
        {
            Id = 21,
            MatchId = 6,
            TeamId = 6,
            GoalScorerId = 22,
            GoalAssistantId = 23,
            GoalMinute = 76
        },
            new Goal
            {
                Id = 22,
                MatchId = 7,
                TeamId = 3,
                GoalScorerId = 12,
                GoalAssistantId = 11,
                GoalMinute = 12
            },
        new Goal
        {
            Id = 23,
            MatchId = 7,
            TeamId = 3,
            GoalScorerId = 11,
            GoalAssistantId = 10,
            GoalMinute = 28
        },
        new Goal
        {
            Id = 24,
            MatchId = 7,
            TeamId = 3,
            GoalScorerId = 10,
            GoalAssistantId = 12,
            GoalMinute = 53
        },
        new Goal
        {
            Id = 25,
            MatchId = 7,
            TeamId = 3,
            GoalScorerId = 9,
            GoalAssistantId = null,
            GoalMinute = 79
        },
        new Goal
        {
            Id = 26,
            MatchId = 7,
            TeamId = 6,
            GoalScorerId = 24,
            GoalAssistantId = 23,
            GoalMinute = 65
        },
        new Goal
        {
            Id = 27,
            MatchId = 9,
            TeamId = 3,
            GoalScorerId = 12,
            GoalAssistantId = 11,
            GoalMinute = 18
        },
        new Goal
        {
            Id = 28,
            MatchId = 9,
            TeamId = 3,
            GoalScorerId = 11,
            GoalAssistantId = 10,
            GoalMinute = 57
        },
        new Goal
        {
            Id = 29,
            MatchId = 9,
            TeamId = 8,
            GoalScorerId = 32,
            GoalAssistantId = 31,
            GoalMinute = 73
        },
        new Goal
        {
            Id = 30,
            MatchId = 10,
            TeamId = 9,
            GoalScorerId = 33,
            GoalAssistantId = 34,
            GoalMinute = 11
        },
        new Goal
        {
            Id = 31,
            MatchId = 10,
            TeamId = 9,
            GoalScorerId = 34,
            GoalAssistantId = 35,
            GoalMinute = 27
        },
        new Goal
        {
            Id = 32,
            MatchId = 10,
            TeamId = 9,
            GoalScorerId = 33,
            GoalAssistantId = null,
            GoalMinute = 49
        },
        new Goal
        {
            Id = 33,
            MatchId = 10,
            TeamId = 9,
            GoalScorerId = 34,
            GoalAssistantId = 33,
            GoalMinute = 68
        },
        new Goal
        {
            Id = 34,
            MatchId = 10,
            TeamId = 4,
            GoalScorerId = 16,
            GoalAssistantId = 15,
            GoalMinute = 78
        },
        new Goal
        {
            Id = 35,
            MatchId = 11,
            TeamId = 10,
            GoalScorerId = 40,
            GoalAssistantId = 39,
            GoalMinute = 32
        },
        new Goal
        {
            Id = 36,
            MatchId = 11,
            TeamId = 10,
            GoalScorerId = 38,
            GoalAssistantId = 40,
            GoalMinute = 71
        },
          new Goal
          {
              Id = 37,
              MatchId = 12,
              TeamId = 4,
              GoalScorerId = 16,
              GoalAssistantId = 15,
              GoalMinute = 11
          },
        new Goal
        {
            Id = 38,
            MatchId = 12,
            TeamId = 4,
            GoalScorerId = 14,
            GoalAssistantId = 16,
            GoalMinute = 34
        },
        new Goal
        {
            Id = 39,
            MatchId = 12,
            TeamId = 4,
            GoalScorerId = 15,
            GoalAssistantId = 14,
            GoalMinute = 78
        },
        new Goal
        {
            Id = 40,
            MatchId = 12,
            TeamId = 5,
            GoalScorerId = 20,
            GoalAssistantId = 19,
            GoalMinute = 22
        },
        new Goal
        {
            Id = 41,
            MatchId = 12,
            TeamId = 5,
            GoalScorerId = 18,
            GoalAssistantId = 20,
            GoalMinute = 45
        },
        new Goal
        {
            Id = 42,
            MatchId = 12,
            TeamId = 5,
            GoalScorerId = 20,
            GoalAssistantId = null,
            GoalMinute = 60
        },
        new Goal
        {
            Id = 43,
            MatchId = 12,
            TeamId = 5,
            GoalScorerId = 17,
            GoalAssistantId = null,
            GoalMinute = 85
        },
        new Goal
        {
            Id = 44,
            MatchId = 13,
            TeamId = 5,
            GoalScorerId = 20,
            GoalAssistantId = 19,
            GoalMinute = 18
        },
        new Goal
        {
            Id = 45,
            MatchId = 13,
            TeamId = 5,
            GoalScorerId = 18,
            GoalAssistantId = 20,
            GoalMinute = 52
        },
        new Goal
        {
            Id = 46,
            MatchId = 13,
            TeamId = 6,
            GoalScorerId = 24,
            GoalAssistantId = 23,
            GoalMinute = 37
        },
        new Goal
        {
            Id = 47,
            MatchId = 13,
            TeamId = 6,
            GoalScorerId = 22,
            GoalAssistantId = 24,
            GoalMinute = 76
        }
            );
        }
    }
}
