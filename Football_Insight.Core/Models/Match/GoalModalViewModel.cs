using Football_Insight.Core.Models.Player;

namespace Football_Insight.Core.Models.Match
{
    public class GoalModalViewModel
    {
        public int PlayerId { get; set; }

        public List<PlayerSimpleViewModel> Players { get; set; } = new List<PlayerSimpleViewModel>();
    }
}
