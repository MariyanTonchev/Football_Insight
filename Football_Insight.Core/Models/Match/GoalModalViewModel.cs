using Football_Insight.Core.Models.Player;

namespace Football_Insight.Core.Models.Match
{
    public class GoalModalViewModel
    {
        public int PlayerScorerId { get; set; }

        public int PlayerAssistedId { get; set; }

        public List<PlayerSimpleViewModel> Players { get; set; } = new List<PlayerSimpleViewModel>();
    }
}
