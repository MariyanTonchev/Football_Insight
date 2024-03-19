using Football_Insight.Data.Models;
using Football_Insight.Models.Coach;
using Football_Insight.Models.League;
using Football_Insight.Models.Match;
using Football_Insight.Models.Player;
using Football_Insight.Models.Stadium;

namespace Football_Insight.Models.Team
{
    public class TeamDetailedViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Founded { get; set; }

        public string LogoURL { get; set; } = string.Empty;

        public CoachSimpleViewModel Coach { get; set; } = null!;

        public LeagueSimpleViewModel League { get; set; } = null!;

        public StadiumSimpleViewModel Stadium { get; set; } = null!;
    }
}