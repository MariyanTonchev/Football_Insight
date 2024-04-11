using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Infrastructure.Data.Models
{
    [Comment("Represent information about goals scored in matches.")]
    public class Goal
    {
        [Key]
        [Comment("Unique identifier for the goal.")]
        public int Id { get; set; }

        [Required]
        [Comment("Identifier of the match in which the goal was scored. Foreign key that references the Match entity.")]
        public int MatchId { get; set; }

        [Required]
        [Comment("Identifier of the team that scored the goal. Foreign key that references the Team entity.")]
        public int TeamId { get; set; }

        [Required]
        [Comment("Identifier of the player who scored the goal. Foreign key that references the Player entity as the goal scorer.")]
        public int GoalScorerId { get; set; }

        [Comment("Optional identifier of the player who assisted the goal. Can be null if there was no assist.")]
        public int? GoalAssistantId { get; set; }

        [Required]
        [Comment("The minute of the match in which the goal was scored.")]
        public int GoalMinute { get; set; }

        //Navigation property for the Match entity. Represents the match context for the goal.
        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; } = null!;

        //Navigation property for the Team entity.Represents the team that scored the goal.
        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; } = null!;

        //Navigation property for the Player entity as the goal scorer. Represents the player who scored the goal.
        [ForeignKey(nameof(GoalScorerId))]
        public virtual Player GoalScorer { get; set; } = null!;

        //Optional navigation property for the Player entity as the goal assistant. Represents the player who assisted the goal, if applicable."
        public virtual Player? GoalAssistant { get; set; }
    }
}
