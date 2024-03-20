using Football_Insight.Core.Models.Match;
using Football_Insight.Core.Models.Team;

namespace Football_Insight.Core.Models.League
{
    public class LeagueMatchesViewModel
    {
        public List<TeamTableViewModel> Teams { get; set; } = new List<TeamTableViewModel>();
        public List<MatchLeagueViewModel> Matches { get; set; } = new List<MatchLeagueViewModel>();
    }
}
