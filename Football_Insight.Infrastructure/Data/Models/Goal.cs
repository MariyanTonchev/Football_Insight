using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Infrastructure.Data.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public int GoalScorerId { get; set; }

        [Required]
        public int GoalAssistantId { get; set; }

        [Required]
        public int GoalMinute { get; set; }

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;

        [ForeignKey(nameof(GoalScorerId))]
        public Player GoalScorer { get; set; } = null!;

        [ForeignKey(nameof(GoalAssistantId))]
        public Player GoalAssistant { get; set; } = null!;
    }
}
