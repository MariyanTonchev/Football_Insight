using Football_Insight.Core.Models.League;
using Football_Insight.Core.Models.Stadium;

namespace Football_Insight.Core.Models.Team
{
    public class TeamDetailedViewModel
    {
        public int TeamId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Founded { get; set; }

        public string LogoURL { get; set; } = string.Empty;

        public string Coach { get; set; } = string.Empty;

        public LeagueSimpleViewModel League { get; set; } = null!;

        public StadiumSimpleViewModel Stadium { get; set; } = null!;
    }
}