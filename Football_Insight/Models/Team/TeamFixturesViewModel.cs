using Football_Insight.Models.Match;

namespace Football_Insight.Models.Team
{
    public class TeamFixturesViewModel
    {
        public int TeamId { get; set; }

        public List<MatchFixtureViewModel> Fixtures { get; set; } = new List<MatchFixtureViewModel>();
    }
}
