using Football_Insight.Core.Models.Player;

namespace Football_Insight.Core.Models.Team
{
    public class TeamSquadViewModel
    {
        public int TeamId { get; set; }

        public List<PlayerSquadViewModel> Players { get; set; } = new List<PlayerSquadViewModel>();
    }
}
