using Football_Insight.Models.Player;

namespace Football_Insight.Models.Team
{
    public class TeamSquadViewModel
    {
        public int TeamId { get; set; }

        public List<PlayerSquadViewModel> Players { get; set; } = new List<PlayerSquadViewModel>();
    }
}
