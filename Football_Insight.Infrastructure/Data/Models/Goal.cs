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

        public int? GoalAssistantId { get; set; }

        [Required]
        public int GoalMinute { get; set; }

        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; } = null!;

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; } = null!;

        [ForeignKey(nameof(GoalScorerId))]
        public virtual Player GoalScorer { get; set; } = null!;

        public virtual Player? GoalAssistant { get; set; }
    }
}
