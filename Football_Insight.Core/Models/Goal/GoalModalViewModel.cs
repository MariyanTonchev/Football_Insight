using Football_Insight.Core.Models.Player;
using System.ComponentModel.DataAnnotations;

namespace Football_Insight.Core.Models.Goal
{
    public class GoalModalViewModel
    {
        [Required]
        public int PlayerScorerId { get; set; }

        public int PlayerAssistedId { get; set; }

        public int MatchId { get; set; }

        public int GoalMinute { get; set; }

        public int TeamId { get; set; }

        public List<PlayerSimpleViewModel> Players { get; set; } = new List<PlayerSimpleViewModel>();
    }
}
