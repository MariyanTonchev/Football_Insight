using System.ComponentModel.DataAnnotations.Schema;

namespace Football_Insight.Data.Models
{
    public class PlayerMatch
    {
        public int PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; } = null!;

        public int MatchId { get; set; }

        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;
    }
}
