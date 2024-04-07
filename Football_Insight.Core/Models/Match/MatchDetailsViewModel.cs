using Football_Insight.Infrastructure.Data.Enums;


namespace Football_Insight.Core.Models.Match
{
    public class MatchDetailsViewModel
    {
        public int Id { get; set; }

        public string DateAndTime { get; set; } = string.Empty;

        public string HomeTeamName { get; set; } = string.Empty;

        public int HomeTeamId { get; set; }

        public string AwayTeamName { get; set; } = string.Empty;

        public int AwayTeamId { get; set; }

        public int HomeScore { get; set; }

        public int AwayScore { get; set; }

        public int LeagueId { get; set; }

        public MatchStatus Status { get; set; }

        public int Minutes { get; set; }
    }
}
