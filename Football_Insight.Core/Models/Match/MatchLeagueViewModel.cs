using Football_Insight.Infrastructure.Data.Enums;

namespace Football_Insight.Core.Models.Match
{
    public class MatchLeagueViewModel
    {
        public int Id { get; set; }

        public string HomeTeamName { get; set; } = string.Empty;

        public string AwayTeamName { get; set; } = string.Empty;

        public string DateAndTime { get; set; } = string.Empty;

        public MatchStatus MatchStatus { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; } 

        public bool IsFavorite { get; set; }
    }
}
