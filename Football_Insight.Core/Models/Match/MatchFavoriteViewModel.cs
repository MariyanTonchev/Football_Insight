using Football_Insight.Infrastructure.Data.Enums;

namespace Football_Insight.Core.Models.Match
{
    public class MatchFavoriteViewModel
    {
        public int MatchId { get; set; }

        public string DateAndTime { get; set; } = string.Empty;

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;

        public MatchStatus MatchStatus { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public int LeagueId { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get;set; }
    }
}
