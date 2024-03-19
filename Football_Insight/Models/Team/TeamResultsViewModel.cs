using Football_Insight.Models.Match;

namespace Football_Insight.Models.Team
{
    public class TeamResultsViewModel
    {
        public int TeamId { get; set; }

        public List<MatchResultViewModel> Results { get; set; } = new List<MatchResultViewModel>();
    }
}
