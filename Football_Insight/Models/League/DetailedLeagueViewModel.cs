using Football_Insight.Models.Match;
using Football_Insight.Models.Team;

namespace Football_Insight.Models.League
{
    public class DetailedLeagueViewModel
    {
        public List<TeamTableViewModel> Teams { get; set; } = new List<TeamTableViewModel>();
        public List<MatchLeagueViewModel> Matches { get; set; } = new List<MatchLeagueViewModel>();
    }
}
