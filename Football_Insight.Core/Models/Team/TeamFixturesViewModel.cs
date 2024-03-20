using Football_Insight.Core.Models.Match;

namespace Football_Insight.Core.Models.Team
{
    public class TeamFixturesViewModel
    {
        public int TeamId { get; set; }

        public List<MatchFixtureViewModel> Fixtures { get; set; } = new List<MatchFixtureViewModel>();
    }
}
