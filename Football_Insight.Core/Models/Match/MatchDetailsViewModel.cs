using Football_Insight.Core.Models.Goal;
using Football_Insight.Core.Models.Team;
using Football_Insight.Infrastructure.Data.Enums;


namespace Football_Insight.Core.Models.Match
{
    public class MatchDetailsViewModel
    {
        public int Id { get; set; }

        public string DateAndTime { get; set; } = string.Empty;

        public int LeagueId { get; set; }

        public MatchStatus Status { get; set; }

        public int Minutes { get; set; }

        public int HomeGoals { get; set; }

        public int AwayGoals { get; set; }

        public TeamDetailedViewModel AwayTeam { get; set; } = null!;

        public TeamDetailedViewModel HomeTeam { get; set; } = null!;

        public ICollection<GoalSimpleModelView> Goals { get; set; } = new List<GoalSimpleModelView>();
    }
}
