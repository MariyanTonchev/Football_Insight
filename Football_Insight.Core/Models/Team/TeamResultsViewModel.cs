using Football_Insight.Core.Models.Match;

namespace Football_Insight.Core.Models.Team
{
    public class TeamResultsViewModel
    {
        public int TeamId { get; set; }

        public List<MatchResultViewModel> Results { get; set; } = new List<MatchResultViewModel>();
    }
}
