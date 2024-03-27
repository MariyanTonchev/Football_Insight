using Football_Insight.Core.Models.Team;

namespace Football_Insight.Core.Models.Match
{
    public class MatchDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime DateAndTime { get; set; } = DateTime.Today;

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int LeagueId { get; set; }

        public ICollection<TeamSimpleViewModel> Teams { get; set; } = new List<TeamSimpleViewModel>();

    }
}
